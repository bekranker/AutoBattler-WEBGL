using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Wave Type", menuName = "Create new Wave", order = 1)]
public class WaveType : ScriptableObject
{
    public int TotalEnemy;
    public List<ItemSCB> NewItemsForMarket;
}