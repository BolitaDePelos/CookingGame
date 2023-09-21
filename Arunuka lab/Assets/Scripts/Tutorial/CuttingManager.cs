using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CuttingManager : SingletonMonobehaviour<CuttingManager>
{
    [SerializeField]
    private List<Food> ingredients;

    [SerializeField]
    private int currentItemsCount = 0;

    [SerializeField]
    private int totalItemsCount;

    public bool tutorialMode;

    [SerializeField]
    private List<GameObject> sliceParents;

    public bool onCutDone;

    [SerializeField]
    private UnityEvent OnCutEnds;

    [SerializeField]
    private StroveManager stroveManager;

    public void AddItemCut(Food item)
    {
        if (ingredients.Contains(item))
        {
            currentItemsCount++;
        }

        if (currentItemsCount == totalItemsCount && tutorialMode)
        {
            TutorialManager.Instance.NextText();
        }
    }

    public void AddSliceItem(GameObject parent)
    {
        sliceParents.Add(parent);
    }

    public void CheckCut()
    {
        int sliceParentsCount = 0;
        for (int idx = 0; idx < sliceParents.Count; idx++)
        {
            if (sliceParents[idx].transform.GetChild(2).gameObject.activeInHierarchy)
            {
                sliceParentsCount++;
            }
        }
        if (sliceParentsCount == totalItemsCount && !onCutDone)
        {
            TutorialManager.Instance.NextText();
            onCutDone = true;
            OnCutEnds.Invoke();
            stroveManager.SetSlicesPosition(sliceParents);
        }
    }
}