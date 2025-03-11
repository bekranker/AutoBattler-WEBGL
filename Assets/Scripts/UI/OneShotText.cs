using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class OneShotText : MonoBehaviour 
{

    [SerializeField] private List<string> _linesToSay;
    [SerializeField] private TMP_Text _textBox;
    [SerializeField] private Canvas _canvas;
    void Start()
    {
        _canvas.worldCamera = Camera.main;
        _textBox.text = _linesToSay[Random.Range(0, _linesToSay.Count)];
        Destroy(gameObject, 3f);
    }
}