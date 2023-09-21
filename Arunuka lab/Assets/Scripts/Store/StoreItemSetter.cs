using UnityEngine;

public abstract class StoreItemSetter: MonoBehaviour
{
    [SerializeField] protected CustomObject typeObject;
    public abstract void SetItem(StorePropData data);
    public abstract void AutoSetItem(StoreDataList list);
}
