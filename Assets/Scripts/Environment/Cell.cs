using UnityEngine;


[System.Serializable]
public class Cell
{
    public Transform CellT;
    public bool Busy;
    public IMarketObject CurrentHoldingObject;

    public void SetCell(IMarketObject marketObject)
    {
        if (Busy) return;
        CurrentHoldingObject = marketObject;
        Busy = true;
    }
}