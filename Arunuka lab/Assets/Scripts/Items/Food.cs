using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    public Ingredients IngredientType;
    public string IngredientName;
    public Material crossMaterial;

    [SerializeField]
    private GameObject cuttingBoard;

    [SerializeField]
    private CuttingManager cuttingManager;

    [SerializeField]
    private bool onCuttingBoard;

    public Vector3 initPos;

    private void Start()
    {
        initPos = transform.position;
    }

    public void SetIngredientType(Ingredients type)
    {
        IngredientType = type;
        IngredientName = Enum.GetName(typeof(Ingredients), IngredientType);
    }

    public void SetCrossMaterial(Material material)
    {
        crossMaterial = material;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == cuttingBoard && !onCuttingBoard)
        {
            onCuttingBoard = true;
            cuttingManager.AddItemCut(this);
        }
    }
}