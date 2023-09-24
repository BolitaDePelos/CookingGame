using UnityEngine;

public class StoreItemCurrentSetter
{
    public void SetCurrent(CustomObject typeObject, StorePropData data)
    {
        PlayerPrefs.SetString(SaveProperties.PrefixCurrentProp + typeObject, data.NameItem);
    }

    public string GetCurrentName(CustomObject typeObject)
    {
        return PlayerPrefs.GetString(SaveProperties.PrefixCurrentProp + typeObject, "");
    }
}