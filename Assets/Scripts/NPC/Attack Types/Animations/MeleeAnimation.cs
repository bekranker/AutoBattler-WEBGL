using DG.Tweening;
using UnityEngine;

public class MeleeAnimation : IAttackAnimation
{
    [SerializeField] private float _fightSpeed;

    public string NameForDebug { get; set; }


    public Tween PlayAnimation(GameObject rootObject)
    {
        return rootObject.transform.DOPunchPosition(Vector2.right * Mathf.Sign(Random.Range(-1, 1)), _fightSpeed);
    }
}