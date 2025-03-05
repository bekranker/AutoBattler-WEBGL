using DG.Tweening;
using UnityEngine;

public class ArcherAnimation : IAttackAnimation
{
    [SerializeField] private float _attackSpeed;

    public string NameForDebug { get; set; }

    public Tween PlayAnimation(GameObject rootObject)
    {
        if (rootObject != null)
        {
            DOTween.Kill(rootObject.transform);
        }
        return rootObject.transform.DOPunchPosition(Vector2.right * Mathf.Sign(Random.Range(-1, 1)), _attackSpeed);
    }
}