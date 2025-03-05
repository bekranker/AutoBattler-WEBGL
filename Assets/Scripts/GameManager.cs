using System.Collections.Generic;
using TMPro;
using UnityEngine;


/// <summary>
/// C mean class, CMP means Components
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _gameSpeedTMP;
    [SerializeField] private ButtonEffect _gameSpeedButton;
    [SerializeField] List<float> _gameSpeed = new();
    private int _gameSpeedIndex;
    public Holder C_EnemyHolder;
    public Holder C_PlayerHolder;
    public Spawner PlayerSpawner;
    public Spawner EnemySpawner;
    public BattleSystem C_BattleSystem;
    public WaveSystem C_WaveSystem;
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        C_WaveSystem.Initialize();
    }
    void OnEnable()
    {
        _gameSpeedButton.OnClick += ChangeGameSpeed;
    }
    void OnDisable()
    {
        _gameSpeedButton.OnClick -= ChangeGameSpeed;
    }
    public void ChangeGameSpeed()
    {
        if (_gameSpeedIndex + 1 < _gameSpeed.Count)
        {
            _gameSpeedIndex++;
        }
        else
            _gameSpeedIndex = 0;

        _gameSpeedTMP.text = _gameSpeed[_gameSpeedIndex].ToString() + "x";
        Time.timeScale = _gameSpeed[_gameSpeedIndex];
    }
}