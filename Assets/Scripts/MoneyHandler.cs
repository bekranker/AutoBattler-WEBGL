using DG.Tweening;
using TMPro;
using UnityEngine;

public class MoneyHandler : MonoBehaviour
{
    public float CurrnetMoney;
    [SerializeField] private TMP_Text _moneyTMP;
    public static MoneyHandler Instance;

    void Awake()
    {
        ChangeTMP();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(this);
        }
        else
            Destroy(this);
    }
    public void IncreaseMoney(float amount)
    {
        CurrnetMoney += amount;
        ChangeTMP();
        SaveManager.SaveMoney((int)CurrnetMoney);
    }
    public bool DecreaseMoney(float amount)
    {
        if (amount >= CurrnetMoney)
        {
            IAmBroke();
            return false;
        }
        CurrnetMoney -= amount;
        ChangeTMP();
        SaveManager.SaveMoney((int)CurrnetMoney);
        return true;
    }
    public Tween MoveTowardsUI(GameObject coin)
    {
        return coin.transform.DOMove(_moneyTMP.transform.position, DoTweenProps.Instance.MoveSpeed);
    }
    public void IAmBroke()
    {
        CurrnetMoney = 0;
        ChangeTMP();
        SaveManager.SaveMoney((int)CurrnetMoney);
    }
    private void ChangeTMP()
    {
        _moneyTMP.text = "Coin: " + CurrnetMoney.ToString();
        DOTween.Kill(_moneyTMP);
        DOTween.Kill(_moneyTMP.transform);
        _moneyTMP.transform.localScale = Vector3.one;
        _moneyTMP.transform.DOPunchScale(Vector3.one * .3f, .2f);
    }
}