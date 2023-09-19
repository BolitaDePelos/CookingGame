using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MoneyDisplayer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textUI = null;
    [SerializeField] private string prefix = "";
    MoneyReader moneyReader;
    private void Awake() => moneyReader = new MoneyReader();
    private void OnEnable() => MoneyUpdater.OnUpdateMoney += (i) => DisplayMoney();
    private void OnDisable() => MoneyUpdater.OnUpdateMoney -= (i) => DisplayMoney();
    public void DisplayMoney() 
    {
        var money = moneyReader.GetMoney();
        textUI.text = prefix + money.ToString();
    }
}
