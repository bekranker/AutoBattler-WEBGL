using UnityEngine;

public class Enemy : NPC
{
    public Coin CoinPrefab;


    void OnEnable()
    {
        OnDie += SpawnCoin;
    }
    void OnDisable()
    {
        OnDie -= SpawnCoin;
    }
    void SpawnCoin()
    {
        ICollectable spawnedCoin = Instantiate(CoinPrefab, transform.position, Quaternion.identity);
        spawnedCoin.ForceMe();
    }
}