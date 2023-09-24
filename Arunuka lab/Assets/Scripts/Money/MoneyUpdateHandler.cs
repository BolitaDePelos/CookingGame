using System;
using UnityEngine;

public class MoneyUpdateHandler : MonoBehaviour
{
    private MoneyUpdater updater;

    private void Start()
    {
        updater = new MoneyUpdater();
    }

    public void UpdateMoney(int amount)
    {
        updater.UpdateMoney(amount);
    }
}

public class MoneyUpdater
{
    public static event Action<int> OnUpdateMoney;
    private readonly MoneyReader moneyReader;

    public MoneyUpdater()
    {
        moneyReader = new MoneyReader();
    }

    public void UpdateMoney(int amount)
    {
        int money = moneyReader.GetMoney();
        money += amount;
        PlayerPrefs.SetInt(SaveProperties.ScoreProperty, money);
        OnUpdateMoney?.Invoke(money);
    }
}