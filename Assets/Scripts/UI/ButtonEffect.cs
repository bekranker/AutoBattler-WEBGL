using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonEffect : MonoBehaviour, IPointerEnterHandler, IPointerDownHandler, IPointerUpHandler, IPointerExitHandler
{
    private Vector3 _initialScale;
    private float _duration { get => DoTweenProps.Instance.ButtonEffectDuration; }
    public event Action OnClick;
    void Start()
    {
        _initialScale = transform.localScale;
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        transform.DOScale(DoTweenProps.Instance.OnDownScale, _duration);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        transform.DOScale(DoTweenProps.Instance.OnHoverEnterScale, _duration);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        transform.DOScale(_initialScale, _duration);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        DOTween.Kill(transform);
        OnClick?.Invoke();
        transform.DOPunchScale(DoTweenProps.Instance.WithRandom(DoTweenProps.Instance.ButtonPunch, 1.3f), _duration);
    }
}