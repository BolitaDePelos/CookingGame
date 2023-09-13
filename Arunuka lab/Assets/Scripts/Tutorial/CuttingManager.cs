using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingManager : MonoBehaviour
{

    [SerializeField]
    private List<Food> ingredients;
    [SerializeField]
    private int currentItemsCount=0;

    [SerializeField]
    private int totalItemsCount;
    public bool tutorialMode;
    public void AddItemCut(Food item) {
        if (ingredients.Contains(item)) {
            currentItemsCount++;
        }

        if (currentItemsCount== totalItemsCount && tutorialMode) {
            TutorialManager.Instance.NextText();
        }
    }
}
