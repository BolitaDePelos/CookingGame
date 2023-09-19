using UnityEngine;

public class StoreItemCurrentSetter 
{
    public void SetCurrent(CustomObject typeObject, StorePropData data) => PlayerPrefs.SetString(KeyStorage.prefixCurrentProp + typeObject, data.NameItem);

    public string GetCurrentName(CustomObject typeObject) => PlayerPrefs.GetString(KeyStorage.prefixCurrentProp + typeObject, "");
}
