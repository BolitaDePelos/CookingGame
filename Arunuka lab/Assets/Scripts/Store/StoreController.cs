using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class StoreController : Singleton<StoreController>
{
    [SerializeField] private ItemStoreHandler[] items;
    [SerializeField] CinemachineVirtualCamera vcam = null;
    [SerializeField] private CostItemDisplay costDisplayer;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private Button buttonSelect;
    int currentItem =0;
    MoneyUpdater moneyUpdater;
    public ItemStoreHandler CurrentItem => items[currentItem];

    private void Start()
    {
        moneyUpdater = new MoneyUpdater();
        SetItem(0);
        buttonBuy.onClick.AddListener(BuyItem);
        buttonSelect.onClick.AddListener(SelectItem);
    }

    private void BuyItem()
    {
        CurrentItem.UnlockItem();
        moneyUpdater.UpdateMoney(-CurrentItem.Price);
    }

    public void SelectItem() => CurrentItem.SetCurrent();

    public void NextItem() 
    {
        currentItem++;
        if (currentItem >= items.Length)
            currentItem = 0;
        DisplayItem();
    }

    public void BackItem()
    {
        currentItem--;
        if (currentItem <0)
            currentItem = items.Length-1;
        DisplayItem();
    }


    public void SetItem(int newIndex) 
    {
        currentItem = newIndex;
        DisplayItem();
    }

    public void DisplayItem() 
    {
        costDisplayer.DisplayCost();
        buttonBuy.interactable =  CurrentItem.CheckBuy();
        buttonSelect.interactable = CurrentItem.CheckSelect();
        vcam.Follow = CurrentItem.transform;
    }

    [MenuItem("Tools/PlayerPrefs Elimination")]
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }
}
