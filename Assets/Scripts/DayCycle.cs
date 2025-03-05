using System;
using UnityEngine;
using UnityEngine.UI;

public class DayCycle : MonoBehaviour
{
    [Header("----Props")]
    [SerializeField] private Sprite MorningSprite;
    [SerializeField] private Sprite NightSprite;

    public DayState MyDayType = DayState.Morning;
    /// <summary>
    /// changing prices, maybe adding some sound effects
    /// </summary>
    public static event Action AtEvening, AtMorning;


    [Header("----Components")]
    [SerializeField] private Image _dayStateImage;


    //changin day to another state
    public void ChangeDay()
    {
        if (MyDayType == DayState.Morning)
        {
            MyDayType = DayState.Evening;
            _dayStateImage.sprite = NightSprite;
            AtEvening?.Invoke();
        }
        else
        {
            MyDayType = DayState.Morning;
            _dayStateImage.sprite = MorningSprite;
            AtMorning?.Invoke();
        }
    }
}
public enum DayState
{
    Morning,
    Evening
}