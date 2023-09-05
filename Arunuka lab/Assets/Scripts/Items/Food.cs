using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{

    public Ingredients vegetableType;
    public string vegetableName;
    public Material crossMaterial;
    public void SetVegetableType(Ingredients type)
    {
        vegetableType = type;
        switch (vegetableType)
        {
            case Ingredients.Carrot:
                vegetableName = "Carrot";
                break;
            case Ingredients.Onion:
                vegetableName = "Onion";

                break;
            case Ingredients.Garlic:
                vegetableName = "Garlic";

                break;
        }
    }

    public void SetCrossMaterial(Material material)
    {
        crossMaterial = material;
    }




}
