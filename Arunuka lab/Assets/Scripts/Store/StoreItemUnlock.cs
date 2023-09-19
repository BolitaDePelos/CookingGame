using UnityEngine;

public class StoreItemUnlock
{
    public void UnlockItemByName(StorePropData data, CustomObject typeObject) => PlayerPrefs.SetInt(typeObject.Name + "_" + data.NameItem, 1);
    public void UnlockItemByName(string data, string typeObject) => PlayerPrefs.SetInt(typeObject + "_" + data, 1);

    public bool HasItem(StorePropData data, CustomObject typeObject) => PlayerPrefs.HasKey(typeObject.Name + "_" + data.NameItem);
}
