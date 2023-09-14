using Newtonsoft.Json.Bson;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using UnityEngine;

public class CuttingManager : MonoBehaviour
{

    [SerializeField]
    private List<Food> ingredients;
    [SerializeField]
    private int currentItemsCount = 0;

    [SerializeField]
    private int totalItemsCount;
    public bool tutorialMode;


    [SerializeField]
    private GameObject knife;
    [SerializeField]
    private List<GameObject> sliceParents;
    public bool onCutDone;
    public void AddItemCut(Food item) {
        if (ingredients.Contains(item)) {
            currentItemsCount++;
        }

        if (currentItemsCount== totalItemsCount && tutorialMode) {
            TutorialManager.Instance.NextText();
        }
    }

    public void AddSliceItem(GameObject parent) {
        sliceParents.Add(parent);
    }

    public void CheckCut()
    {
        int sliceParentsCount=0;
        for (int idx=0;idx<sliceParents.Count;idx++) {
            if (sliceParents[idx].gameObject.transform.GetChild(2).gameObject.activeInHierarchy){
                sliceParentsCount++;
            }

        }
        if (sliceParentsCount==totalItemsCount&&! onCutDone) {
            TutorialManager.Instance.NextText();
            onCutDone = true;

        }

    }
}
