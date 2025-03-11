using System;
using System.Collections.Generic;
using UnityEngine;
public class PunchManager : MonoBehaviour
{
    [Header("---DoTween Props")]
    [SerializeField] private float _punchDuration;
    [SerializeField] private Vector3 _punchScale;
    [Header("---Spawn Objcets")]
    [SerializeField] private GameObject _bekirPrefab;
    [SerializeField] private Punch _punchPrefab;
    [Header("---Components")]
    [SerializeField] private Transform _bekir;

    private GameObject _currentBekir;
    void Start(){
        SpawnBekirAndDeleteOtherOne();
    }
    void OnEnable()
    {
        ScreenManager.OnClick += SpawnPunch;
    }
    void OnDisable()
    {
        ScreenManager.OnClick -= SpawnPunch;
    }
    void SpawnPunch(Vector2 startPosition)
    {
        if(_bekir == null) return;
        
        Punch spawnedPunch = Instantiate(_punchPrefab, startPosition, Quaternion.identity);
        spawnedPunch.Init(_bekir);
    }
    public void SpawnBekirAndDeleteOtherOne()
    {
        if(_currentBekir != null)
            Destroy(_currentBekir);
        _currentBekir = Instantiate(_bekirPrefab, Vector2.zero, Quaternion.identity);
        _bekir = _currentBekir.transform.GetChild(0).transform.GetChild(18).transform;
    }
}