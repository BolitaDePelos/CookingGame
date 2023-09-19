using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoneyStoreController : MonoBehaviour
{
    [SerializeField] private MoneyDisplayer moneyDisplayer;
    void Start() => moneyDisplayer.DisplayMoney();

}
