using B_Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TableManager:Singleton<TableManager>,IKitchenSector
{
    [SerializeField] Transform centerFridge;
    [SerializeField] Transform tableGreen;
    [SerializeField] Transform tableCenter;
    [SerializeField] Knife knife;
    int count = 0;

    public List<Food> foodsOnTable = new List<Food>();

    private void Start()
    {
        knife.onPickUp.AddListener(SetPosCenter);
    }


    public void ConfigureFoodKitchenSector(Food food)
    {
        foodsOnTable.Add(food);
        StartCoroutine(WaitBuffer(food));
        count++;
    }

    IEnumerator WaitBuffer(Food food) 
    {
        yield return new WaitForSeconds(0.3f*count);
        food.AnimateMovement(new Transform[] { centerFridge, tableGreen });
        food.SetFoodLocation(FoodLocation.Table);
    }

    public void SetPosCenter() 
    {
        if(foodsOnTable.Count>0)
            foodsOnTable[0].AnimateMovement(new Transform[] { tableCenter });
    }
}
