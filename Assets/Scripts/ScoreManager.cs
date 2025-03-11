using System;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private float _scoreAmount;
    [SerializeField] private Vector3 _punchScale;
    [SerializeField] private float _punchDuration;
    [SerializeField] private TMP_Text _scoreTMP;

    private float _scoreCounter;

    void OnEnable()
    {
        ScreenManager.OnClick += OnScoreChange;
    }
    void OnDisable()
    {
        ScreenManager.OnClick -= OnScoreChange;
    }
    void OnScoreChange(Vector2 pos)
    {
        _scoreCounter += _scoreAmount;
        _scoreTMP.text = _scoreCounter.ToString();
        DOTween.Kill(_scoreTMP.transform);

        _scoreTMP.transform.localScale = Vector2.one;
        _scoreTMP.transform.DOPunchScale(_punchScale, _punchDuration);
    }
}