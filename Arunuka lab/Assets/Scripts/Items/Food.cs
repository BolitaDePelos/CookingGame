using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{

    public Ingredients IngredientType;
    public string IngredientName;
    public Material crossMaterial;
    public void SetIngredientType(Ingredients type)
    {
        IngredientType = type;
        switch (IngredientType)
        {
            case Ingredients.Carrot:
                IngredientName = "Carrot";
                break;
            case Ingredients.Onion:
                IngredientName = "Onion";
                break;
            case Ingredients.Garlic:
                IngredientName = "Garlic";
                break;
            case Ingredients.Celery:
                IngredientName = "Celery";
                break;
            case Ingredients.Pumpkin:
                IngredientName = "Pumpkin";
                break;
            case Ingredients.Scallops:
                IngredientName = "Scallops";
                break;
            case Ingredients.Quail:
                IngredientName = "Quail";
                break;
        }
    }

    public void SetCrossMaterial(Material material)
    {
        crossMaterial = material;
    }




}
