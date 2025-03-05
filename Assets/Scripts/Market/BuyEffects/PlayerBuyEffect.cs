using UnityEngine;

public class PlayerBuyEffect : IBuyEffect
{
    public bool ExecuteEffect(Transform targetSeat)
    {
        return GameManager.Instance.PlayerSpawner.SpawnToSeat(targetSeat);
    }
}