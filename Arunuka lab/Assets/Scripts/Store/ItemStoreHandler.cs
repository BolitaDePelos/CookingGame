using UnityEngine;
using UnityEngine.UI;

public  class ItemStoreHandler: MonoBehaviour
{
    [SerializeField] private StorePropData data = null;
    [SerializeField] private CustomObject objectType = null;
    [field:SerializeField] public int Price { get; private set; }

    StoreItemUnlock unlocker;
    StoreItemCurrentSetter currentSetter;
    MoneyReader moneyReader;

    private void Awake()
    {
        unlocker = new StoreItemUnlock();
        currentSetter = new StoreItemCurrentSetter();
        moneyReader = new MoneyReader();
    }
    public bool CheckBuy() => moneyReader.CheckHasMoney(Price);

    public bool CheckSelect()=> unlocker.HasItem(data, objectType);

    public void UnlockItem()
    {
        unlocker.UnlockItemByName(data, objectType);
        SetCurrent();
    }

    public void SetCurrent() => currentSetter.SetCurrent(objectType, data);
}
