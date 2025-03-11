using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Instance { get; set; }
    public bool Paused = false;
    public static event Action OnPause, OnContunieToPlay;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        if (Paused)
        {
            PauseTheGame();
        }
        else
        {
            ContunieToPlay();
        }
    }
    public void PauseTheGame()
    {
        OnPause?.Invoke();
        Time.timeScale = 0;
        Paused = true;
    }
    public void ContunieToPlay()
    {
        OnContunieToPlay?.Invoke();
        Time.timeScale = 1;
        Paused = false;
    }
}