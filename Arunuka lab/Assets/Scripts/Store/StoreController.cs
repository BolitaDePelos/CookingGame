using System.Collections;
using Cinemachine;
using NaughtyAttributes;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StoreController : Singleton<StoreController>
{
    [SerializeField] private ItemStoreHandler[] items;
    [SerializeField] private CinemachineVirtualCamera vcam;
    [SerializeField] private CostItemDisplay costDisplayer;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private Button buttonSelect;

    [Header("Next Day")] [Scene] [SerializeField]
    private int nextDayScene;

    [SerializeField] private Animator fadeOutAnimator;

    private int currentItem;
    private MoneyUpdater moneyUpdater;
    private static readonly int startTriggerId = Animator.StringToHash("Start");
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
    }

    public void BackItem()
    {
        currentItem--;
        if (currentItem < 0)
            currentItem = items.Length - 1;
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
        buttonBuy.interactable = CurrentItem.CheckBuy();
        buttonSelect.interactable = CurrentItem.CheckSelect();
        vcam.Follow = vcam.LookAt = CurrentItem.transform;
    }

    /// <summary>
    /// Goes to the next day.
    /// </summary>
    public void OnGoNextDay()
    {
        StartCoroutine(GoNextDay());
    }

    private IEnumerator GoNextDay()
    {
        fadeOutAnimator.SetTrigger(startTriggerId);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(nextDayScene);
    }


#if UNITY_EDITOR

    [MenuItem("Tools/PlayerPrefs Elimination")]
    public static void DeleteAll()
    {
        PlayerPrefs.DeleteAll();
    }

#endif
}