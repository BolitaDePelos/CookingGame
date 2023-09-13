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
    public void AddItemCut(Food item) {
        if (ingredients.Contains(item)) {
            currentItemsCount++;
        }

        if (currentItemsCount== totalItemsCount && tutorialMode) {
            TutorialManager.Instance.NextText();
            ActiveKnife();
        }
    }

    public void ActiveKnife() {
        knife.GetComponent<BoxCollider>().enabled = true;
        knife.GetComponent<Rigidbody>().isKinematic = true;
    }
}
