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

    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        moneyUpdater = new MoneyUpdater();
        SetItem(0);
        buttonBuy.onClick.AddListener(BuyItem);
        buttonSelect.onClick.AddListener(SelectItem);
    }

    private void BuyItem()
    {
        CurrentItem.UnlockItem();
        moneyUpdater.UpdateMoney(-CurrentItem.Price);
        audioManager.PlayBuySound();
        DisplayItem();
    }

    public void SelectItem()
    {
        CurrentItem.SetCurrent();
        DisplayItem();
    }

    public void NextItem() 
    {
        currentItem++;
        if (currentItem >= items.Length)
            currentItem = 0;
        DisplayItem();
        audioManager.PlayNextPageSound();
    }

    public void BackItem()
    {
        currentItem--;
        if (currentItem <0)
            currentItem = items.Length-1;
        DisplayItem();
        audioManager.PlayNextPageSound();
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
        buttonBuy.gameObject.SetActive(!buttonSelect.interactable);
        buttonSelect.gameObject.SetActive(buttonSelect.interactable);
        vcam.Follow = vcam.LookAt = CurrentItem.transform;
    }


#if UNITY_EDITOR

    [MenuItem("Tools/PlayerPrefs Elimination")]
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

#endif
}
