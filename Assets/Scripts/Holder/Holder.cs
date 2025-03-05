using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class Holder : MonoBehaviour
{
    [SerializeField] public List<Place> _places = new();
    public List<Place> HolderHand;
    public bool IsFullBusy { get => HolderHand.Count >= _places.Count; }
    public bool IsEmpty { get => HolderHand.Count <= 0; }
    public IGridObject<NPCType> FirstGridObject;
    [SerializeField] private BattleSystem _battleSystem;
    [SerializeField] private bool _rootStarter;
    public void Init()
    {
        _places?.ForEach((place) => { place.Init(false); });
    }
    /// <summary>
    /// putting a grid Object to a Place that includes Transform
    /// </summary>

    [Button("Clear Holder")]
    public void DebugInspector(){
        foreach(Place item in HolderHand)
        {
            Destroy((item.CurrentGridObject as MonoBehaviour).gameObject);
        }
        HolderHand.Clear();
        _places.ForEach((item)=>item.Busy = false);
    }
    public void TakeASeat(IGridObject<NPCType> npc)
    {
        if (IsFullBusy) return;
        foreach (Place place in _places)
        {
            if (!place.Busy)
            {
                place.SetGridObject(npc);
                HolderHand.Add(place);
                return;
            }
        }
    }
    public bool TakeASeat(IGridObject<NPCType> npc, Transform targetSeat){
        if (IsFullBusy) return false;
        foreach (Place place in _places)
        {
            if (place.Position == targetSeat)
            {
                if(place.Busy) return false;
                place.SetGridObject(npc);
                HolderHand.Add(place);
                return true;
            }
        }
        return false;
    }
    public void SetEmpty(IGridObject<NPCType> gridObject)
    {
        for (int i = 0; i < _places.Count; i++)
        {
            if (_places[i].CurrentGridObject == gridObject)
            {
                _places[i].CurrentGridObject = null;
                _places[i].Busy = false; // <-- Burayı ekledik
            }
        }
        for (int i = 0; i < HolderHand.Count; i++)
        {
            if (HolderHand[i].CurrentGridObject == gridObject)
            {
                HolderHand.RemoveAt(i);
            }
        }
    }

    [Button("Shift me")]
    public void ShiftLine()
    {
        if (IsEmpty) return;

        List<IGridObject<NPCType>> newOrder = new List<IGridObject<NPCType>>();

        // 📌 Önce dolu olanları sıraya ekle
        foreach (var place in _places)
        {
            if (place.CurrentGridObject != null)
            {
                newOrder.Add(place.CurrentGridObject);
            }
        }

        // 📌 Boş yerleri ekle (null)
        int emptyCount = _places.Count - newOrder.Count;
        for (int i = 0; i < emptyCount; i++)
        {
            newOrder.Add(null);
        }

        // 📌 Yeni sıraya göre NPC'leri yerleştir
        for (int i = 0; i < _places.Count; i++)
        {
            _places[i].CurrentGridObject = newOrder[i];

            if (newOrder[i] != null)
            {
                _places[i].SetGridObject(newOrder[i]);  // NPC'yi yerine taşı
            }
            else
            {
                _places[i].Busy = false;
            }
        }

        // 🎯 HolderHand listesini güncelle
        HolderHand.Clear();
        foreach (var place in _places)
        {
            if (place.CurrentGridObject != null)
            {
                HolderHand.Add(place);
            }
        }

        // // ✅ Shift sonrası doğrulama logları
        // Debug.Log("Shift sonrası sıra:");
        // for (int i = 0; i < _places.Count; i++)
        // {
        //     Debug.Log($"Place {i} -> {(_places[i].CurrentGridObject != null ? "Dolu" : "Boş")}");
        // }

        // FirstGridObject Güncelle
        FirstGridObject = _places[0].CurrentGridObject;

        // if (FirstGridObject == null)
        // {
        //     Debug.LogError("Shift sonrası FirstGridObject hala null! NPC'ler yanlış kaydırılmış olabilir.");
        // }
        if (_rootStarter)
        {
            print("triggered");
            _battleSystem.CanBattleStart = true;
            _battleSystem.StartAttack();
        }
    }

}
[System.Serializable]
public class Place
{
    public bool Busy;
    public Transform Position;
    public int Index;
    public IGridObject<NPCType> CurrentGridObject;

    public void Init(bool busy = false, Transform position = null, int index = 0, IGridObject<NPCType> gridObject = null)
    {
        Index = index;
        Busy = busy;
        CurrentGridObject = gridObject;
        Position = position;
    }
    public void SetGridObject(IGridObject<NPCType> gridObject)
    {
        CurrentGridObject = gridObject;
        // Correctly set the position using the MonoBehaviour's transform
        MonoBehaviour npcMono = gridObject as MonoBehaviour;
        if (npcMono != null)
        {
            npcMono.transform.DOMove(Position.position, .2f).SetEase(Ease.Linear);
        }
        Busy = true;
    }
}
