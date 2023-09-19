using UnityEngine;

public class StoreDataList: Singleton<StoreDataList>
{
    [SerializeField] private StorePropData[] storePropData = null;

    public StorePropData GetItemByName(string _name) 
    {
        return System.Array.Find(storePropData, i => i.NameItem.Equals(_name));
    }
}
