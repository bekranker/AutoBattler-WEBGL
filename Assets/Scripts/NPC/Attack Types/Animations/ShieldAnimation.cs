using DG.Tweening;
using UnityEngine;

public class ShieldAnimation : IAttackAnimation
{
    [SerializeField] private float _moveSpeed;
    [SerializeField, Range(0.05f, 0.1f)] private float _fastMove;

    private float _startPosY;
    public string NameForDebug { get; set; }


    public Tween PlayAnimation(GameObject rootObject)
    {
        if (rootObject != null)
        {
            DOTween.Kill(rootObject.transform);
            if (_startPosY == 0)
                _startPosY = rootObject.transform.position.y;
            rootObject.transform.position = new Vector2(rootObject.transform.position.x, _startPosY);
        }
        Sequence mySequence = DOTween.Sequence();

        float nextheight = rootObject.transform.position.y + 1;

        mySequence.Append(rootObject.transform.DOMoveY(nextheight, _moveSpeed));
        mySequence.Append(rootObject.transform.DOMoveY(_startPosY, _fastMove));

        return mySequence;

    }
}
