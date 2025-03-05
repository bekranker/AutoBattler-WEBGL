using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Collections.Generic;
public class BattleSystem : MonoBehaviour
{
    [Header("----Components")]
    [SerializeField] private Holder _enemyHolder;
    [SerializeField] private Holder _playerHolder;
    [SerializeField] private WaveSystem _waveSystem;

    [Header("----Pivots")]
    [SerializeField] private Transform _playerPivot;
    [SerializeField] private Transform _enemyPivot;

    [Header("----DoTween Props")]
    [SerializeField] private float _gridMoveSpeed;

    private NPC _currentPlayer, _currentEnemy;
    private IGridObject<NPCType> _currentPlayerGridObject, _currentEnemyGridObject;
    public bool CanBattleStart = true;
    Sequence mySequence;
    [Button("Start Fight")]
    public void StartAttack()
    {
        mySequence = DOTween.Sequence();
        _currentEnemyGridObject = _enemyHolder._places[0].CurrentGridObject;
        _currentPlayerGridObject = _playerHolder._places[0].CurrentGridObject;

        if (_currentPlayerGridObject == null || _currentEnemyGridObject == null)
        {
            Debug.LogWarning("Error: One or both grid objects are null!");
            LoseOrWin();
            return;
        }

        _currentPlayer = _currentPlayerGridObject as NPC;
        _currentEnemy = _currentEnemyGridObject as NPC;

        if (_currentPlayer == null || _currentEnemy == null)
        {
            Debug.LogError("Error: One or both NPCs are null! Check if they are correctly assigned.");
            return;
        }
        MoveALittleBit();
    }
    /// <summary>
    /// Checking win first
    /// </summary>
    public void LoseOrWin()
    {
        if (_enemyHolder.IsEmpty)
        {
            Debug.Log("Win");
            return;
        }
        if (_playerHolder.IsEmpty)
        {
            Debug.Log("Lose");
            return;
        }
    }
    [Button("Stop Battle")]
    public void StopBattle()
    {
        _currentPlayer.StopAttacking();
        _currentEnemy.StopAttacking();
    }
    private void MoveALittleBit()
    {
        mySequence = DOTween.Sequence();

        mySequence.Join(_currentPlayer.transform.DOMove(_playerPivot.position, _gridMoveSpeed));
        mySequence.Join(_currentEnemy.transform.DOMove(_enemyPivot.position, _gridMoveSpeed));

        mySequence.AppendCallback(() =>
        {
            //burada nereye saldıracagını verebiliyoruz. bu kısmı Scriptable'dan index çekip _holder'ların içerisinden _places[targetIndex] ile custom saldırıya başlatabiliriz.
            _currentEnemy.Execute(_currentPlayer);
            _currentPlayer.Execute(_currentEnemy);
            Debug.Log("After movement");
        });

        mySequence.Play();
    }
}