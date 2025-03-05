using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private NPC _prefab;
    [SerializeField] private List<NPCType> _types = new();
    [SerializeField] private Holder _holder;
    [SerializeField] private GameManager _gameManager;

    [Button("Spawn")]
    public void SpawnAnNPC(int count)
    {
        if (_holder.IsFullBusy) return;
        int randEnemy = Random.Range(0, _types.Count);
        for (int i = 0; i < count; i++)
        {
            NPC spawnedNPC = Instantiate(_prefab);
            spawnedNPC.Init(_types[randEnemy], _gameManager, _holder);
            _holder.TakeASeat(spawnedNPC); // Pass the spawned enemy
            if (_holder.IsFullBusy) return;
        }
    }
    public bool SpawnToSeat(Transform spawnSeat)
    {
        int randEnemy = Random.Range(0, _types.Count);
        NPC spawnedNPC = Instantiate(_prefab);
        spawnedNPC.Init(_types[randEnemy], _gameManager, _holder);
        return _holder.TakeASeat(spawnedNPC, spawnSeat);
    }
    public void SpawnNPCs(int count, NPCType type)
    {
        if (_holder.IsFullBusy) return;
        for (int i = 0; i < count; i++)
        {
            NPC spawnedNPC = Instantiate(_prefab);
            spawnedNPC.Init(type, _gameManager, _holder);
            _holder.TakeASeat(spawnedNPC); // Pass the spawned enemy
            if (_holder.IsFullBusy) return;
        }
    }
}