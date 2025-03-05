using UnityEngine;

public static class SaveManager
{
    public static void Save(string key, object value)
    {
        if (value.GetType() == typeof(string))
        {
            Debug.Log("string is saved");
            PlayerPrefs.SetString(key, (string)value);
        }
        if (value.GetType() == typeof(int))
        {
            PlayerPrefs.SetInt(key, (int)value);
        }
        if (value.GetType() == typeof(float))
        {
            PlayerPrefs.SetFloat(key, (float)value);
        }
    }
    public static void SaveWave(int waveIndex) => Save("Wave", waveIndex);
    public static void SaveMoney(int count) => Save("Money", count);
    public static int GetMoney() => PlayerPrefs.GetInt("Money", 100);
    public static int GetWave() => PlayerPrefs.GetInt("Wave", 0);
}