using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class GenericInventory
{
    public List<Cell> Cells = new();
    public Cell GetCell(Transform cellTransform){
        if(Cells.Count == 0) {
            Debug.Log("Inventory is Empty");
            return null;
        }
        foreach (Cell item in Cells)
        {
            if(item.CellT == cellTransform){
                Debug.Log("Market Item is found");
                return item;
            }
            Debug.Log("---Item is Searching---");
        }
        Debug.Log("---Item didn't found---");
        return null;
    }
}