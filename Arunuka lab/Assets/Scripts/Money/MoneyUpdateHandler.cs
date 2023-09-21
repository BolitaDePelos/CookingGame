using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MoneyUpdateHandler : MonoBehaviour
{
    MoneyUpdater updater;
    private void Start()
    {
        updater = new MoneyUpdater();
        updater.UpdateMoney(100);
    }

    public void UpdateMoney(int amount) => updater.UpdateMoney(amount);
}

public class MoneyUpdater
{
    public static event System.Action<int> OnUpdateMoney;
    MoneyReader moneyReader;
    public MoneyUpdater()
    {
        this.moneyReader = new MoneyReader();
    }

    public void UpdateMoney(int amount)
    {
        var money = moneyReader.GetMoney();
        money += amount;
        PlayerPrefs.SetInt(KeyStorage.Money, money);
        OnUpdateMoney?.Invoke(money);
    }
}


