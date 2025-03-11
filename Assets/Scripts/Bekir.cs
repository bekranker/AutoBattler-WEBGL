using System.Collections;
using UnityEngine;

public class Bekir : MonoBehaviour 
{
    [SerializeField] private SpriteRenderer _sp;
    [SerializeField] private Color _hitColor;
    private bool _canCall;
    void Awake(){
        _canCall = true;
    }
    public IEnumerator Hit()
    {
        if(_canCall)
        {
            _sp.color = _hitColor;
            _canCall = false;
            yield return new WaitForSeconds(.2f);
            _sp.color = Color.white;
            _canCall = true;
        }
    }
}