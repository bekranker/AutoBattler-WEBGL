using DG.Tweening;
using UnityEngine;

public class Coin : MonoBehaviour, ICollectable
{
    private bool _didCollected;
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private GameObject _uIEffect;
    [SerializeField] private Rigidbody2D _rb2D;
    public void ForceMe()
    {
        Vector2 randDirection = RandomUtils.GetRandomDirection();
        _rb2D.AddForce(randDirection * DoTweenProps.Instance.PushForce * 100);
    }
    public void CollectMe()
    {
        if (_didCollected) return;
        _sp.transform.DOPunchScale(DoTweenProps.Instance.WithRandom(DoTweenProps.Instance.PunchScale, 1.3f), DoTweenProps.Instance.PunchDuration);
        MoneyHandler.Instance.MoveTowardsUI(gameObject).OnComplete(() => { MoneyHandler.Instance.IncreaseMoney(10); Destroy(gameObject); }).SetEase(Ease.InBack);
        GameObject spawnedCavnas = Instantiate(_uIEffect, transform.position, Quaternion.identity);
        _didCollected = true;
        Destroy(spawnedCavnas, 1f);
    }
}