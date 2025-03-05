using DG.Tweening;
using UnityEngine;

public interface IAttackAnimation
{
    public Tween PlayAnimation(GameObject rootObject);
    public string NameForDebug { get; set; }

}