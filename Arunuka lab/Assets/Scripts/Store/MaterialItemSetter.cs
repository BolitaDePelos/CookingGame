using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialItemSetter : StoreItemSetter
{
    StoreItemCurrentSetter current;
    Renderer render => GetComponent<Renderer>();

    private void Awake()
    {
        current = new StoreItemCurrentSetter(); 
    }

    private void OnEnable() 
    {
        AutoSetItem(StoreDataList.Instance);
    }

    public override void AutoSetItem(StoreDataList list)
    {
        var rawName = current.GetCurrentName(typeObject);
        SetItem(list.GetItemByName(rawName));
    }

    public override void SetItem(StorePropData data)
    {
        if (data.HasMaterial)
            render.material = data.Material;
    }
}
