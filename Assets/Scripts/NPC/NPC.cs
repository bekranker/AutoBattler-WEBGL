using System;
using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;

public abstract class NPC : MonoBehaviour, IGridObject<NPCType>, IDamage, IAttack
{
    public NPCType Data { get; set; }
    private float _attackDelay { get => Data.NPCAttackType.AttackDelay; set => value = default; }
    public float CurrenHealth { get; set; }

    private GameManager _gameManager;
    private Holder _myHolder;
    [Header("----Components")]
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private TMP_Text _healCount;
    [SerializeField] private GameObject _canvas;

    public event Action OnDie;
    public void Die()
    {
        if (_canvas.gameObject != null)
            _canvas.gameObject.SetActive(false);
        CurrenHealth = 0;
        // _spriteRenderer.enabled = false;
        _gameManager.C_BattleSystem.StopBattle();
        _myHolder.SetEmpty(this);
        _myHolder.ShiftLine();
        OnDie?.Invoke();
        Destroy(gameObject);
    }
    /// <summary>
    /// pause the Fight
    /// </summary>
    public void StopAttacking()
    {
        if (gameObject == null) return;
        StopAllCoroutines();
        DOTween.Kill(gameObject);
        DOTween.Kill(transform);
        DOTween.Kill(_spriteRenderer.transform);
        DOTween.Kill(_spriteRenderer);
    }
    public void Hit(float amount)
    {
        if (amount >= CurrenHealth)
        {
            Die();
            ChangeHealthTMP();
            return;
        }
        CurrenHealth -= amount;
        ChangeHealthTMP();
    }

    public void Init(NPCType data, GameManager gameManager, Holder myHolder)
    {
        _myHolder = myHolder;
        _gameManager = gameManager;
        Data = data;
        CurrenHealth = Data.Health;
        _spriteRenderer.sprite = data.NPCSprite;
        ChangeVisual();
    }
    private void ChangeVisual()
    {
        if (_spriteRenderer != null)
        {
            _spriteRenderer.sprite = Data.NPCSprite;
        }
        ChangeHealthTMP();
    }
    private void ChangeHealthTMP()
    {
        _healCount.text = CurrenHealth.ToString();
    }
    public void Execute(IDamage opponent)
    {
        StartCoroutine(AttackWithDelay(opponent));
    }
    private IEnumerator AttackWithDelay(IDamage opponent)
    {
        if (CurrenHealth > 0 && opponent.CurrenHealth > 0)
        {
            //Give Damage To NPC from Here

            Data.NPCAttackType.AttackAnimationType.PlayAnimation(gameObject);
            if (opponent != null)
                opponent.Hit(Data.NPCAttackType.Damage);
            yield return new WaitForSeconds(_attackDelay);
            Execute(opponent);
        }
    }
    void OnDestroy()
    {
        DOTween.Kill(transform);
        DOTween.Kill(_spriteRenderer);
        DOTween.Kill(_spriteRenderer.transform);
    }
}