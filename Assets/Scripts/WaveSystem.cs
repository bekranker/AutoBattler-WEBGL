using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;


public class WaveSystem : MonoBehaviour
{
    [Header("---Components")]
    [SerializeField] private Spawner _enemySpawner;
    [SerializeField] private Holder _enemyHolder;

    [Header("---Props")]
    [SerializeField] private List<WaveType> _waves = new();
    [SerializeField] private List<PackType> _packs = new();

    public event Action OnPassingnNextWave;
    public WaveType CurrentWave;
    private int _waveIndex;
    private int _remainingCount;

    public void Initialize()
    {
        _waveIndex = SaveManager.GetWave();
        _remainingCount = _waves[_waveIndex].TotalEnemy;
        CurrentWave = _waves[_waveIndex];
        SpawnEnemies();
    }
    public void NextWave()
    {
        if (_waveIndex + 1 < _waves.Count) return;
        _waveIndex++;
        SaveManager.SaveWave(_waveIndex);
        _remainingCount = _waves[_waveIndex].TotalEnemy;
        CurrentWave = _waves[_waveIndex];
        OnPassingnNextWave?.Invoke();
    }
    public void SpawnEnemies()
    {
        if (!_enemyHolder.IsEmpty) return;
        if (_remainingCount <= 0)
        {
            NextWave();
            return;
        } // Eğer spawn edilmesi gereken düşman kalmadıysa çık

        int spawnCount = Random.Range(1, _remainingCount + 1); // 1 ile _remainingCount arasında rastgele sayı al
        _remainingCount -= spawnCount;

        ChoosePack(spawnCount);
    }
    private void DayCycle()
    {

    }
    private void ChoosePack(int count)
    {
        // Uygun olan pack'leri filtrele (PackType içinde count kadar düşman içerenleri seç)
        List<PackType> suitablePacks = _packs.FindAll(p => p.NPCPack.Count == count);

        if (suitablePacks.Count == 0)
        {
            Debug.LogWarning($"Uygun pack bulunamadı! {count} düşmanlık pack eksik olabilir.");
            return;
        }

        // Uygun olan pack'lerden rastgele birini seç
        PackType selectedPack = suitablePacks[Random.Range(0, suitablePacks.Count)];

        // Seçilen pack içindeki düşmanları spawn et
        foreach (NPCType npc in selectedPack.NPCPack)
        {
            _enemySpawner.SpawnNPCs(1, npc);
        }
    }
}
