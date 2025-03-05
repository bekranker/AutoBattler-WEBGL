using UnityEngine;

public static class RandomUtils
{
    public static Vector2 GetRandomDirection()
    {
        float angle = Random.Range(0f, 360f);
        return new Vector2(Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad)).normalized;
    }
}
