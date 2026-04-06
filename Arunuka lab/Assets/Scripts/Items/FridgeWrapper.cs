using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class FridgeWrapper:Singleton<FridgeWrapper>
{
    [SerializeField] List<Food> fridgeIngredients = new List<Food>();
    public Fridge[] fridge;

    public Food GetIngredient(Ingredients ingredient) 
    {
        var ing = fridgeIngredients.FirstOrDefault(food => food.IngredientType == ingredient);
        return ing;
    }
}