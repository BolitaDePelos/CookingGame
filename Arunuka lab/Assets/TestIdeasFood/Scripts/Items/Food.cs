using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Food : MonoBehaviour
{

    public VegetableType vegetableType;
    public string vegetableName;
    public Material crossMaterial;
    public void SetVegetableType(VegetableType type)
    {
        vegetableType = type;
        switch (vegetableType)
        {
            case VegetableType.Carrot:
                vegetableName = "Carrot";
                break;
            case VegetableType.Onion:
                vegetableName = "Onion";

                break;
            case VegetableType.Garlic:
                vegetableName = "Garlic";

                break;
        }
    }

    public void SetCrossMaterial(Material material)
    {
        crossMaterial = material;
    }




}
