using System;
using UnityEngine;

public class Food : MonoBehaviour
{
    [Header("Food Properties")]
    public Ingredients IngredientType;

    public string IngredientName;
    public Material crossMaterial;
    [SerializeField] private float cookedThresholdSeconds = 10;
    [SerializeField] private Color cookedColor = Color.yellow;
    [SerializeField] private float burnThresholdSeconds = 20;
    [SerializeField] private Color burnColor = Color.black;
    [SerializeField] private FoodCookState foodState = FoodCookState.Raw;
    [SerializeField] private FoodLocation foodLocation = FoodLocation.Table;

    [Header("Extra Properties")]
    [SerializeField] private MeshRenderer materialRenderer;

    [SerializeField]
    private GameObject cuttingBoard;

    [SerializeField]
    private CuttingManager cuttingManager;

    [SerializeField]
    private bool onCuttingBoard;

    public Vector3 initPos;

    private bool isBeingCooked = false;
    private float cookedDuration = 0.0f;
    private Color originalColor;

    private void Start()
    {
        initPos = transform.position;

        if(materialRenderer == null && TryGetComponent(out MeshRenderer myMeshRenderer))
            materialRenderer = myMeshRenderer;

        originalColor = materialRenderer != null
            ? materialRenderer.material.color
            : Color.white;
    }

    private void Update()
    {
        if (!isBeingCooked)
            return;

        cookedDuration += Time.deltaTime;
        if (cookedDuration > cookedThresholdSeconds)
            foodState = FoodCookState.Cooked;

        if (cookedDuration > burnThresholdSeconds)
            foodState = FoodCookState.Burn;

        switch (foodState)
        {
            // Change color to the cooked one if it's raw.
            //
            case FoodCookState.Raw:
                {
                    float percent = cookedDuration / cookedThresholdSeconds;
                    ChangeColorTo(originalColor, cookedColor, percent);
                    break;
                }

            // Change color to the burnt one if it's cooked.
            //
            case FoodCookState.Cooked:
                {
                    float percent = (cookedDuration - cookedThresholdSeconds)
                        / (burnThresholdSeconds - cookedThresholdSeconds);
                    ChangeColorTo(cookedColor, burnColor, percent);
                    break;
                }
        }
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

    /// <summary>
    /// Sets if the food is being cooked or not.
    /// </summary>
    public bool SetIsBeingCooked(bool isBeingCooked) => this.isBeingCooked = isBeingCooked;

    /// <summary>
    /// Sets the location of the food.
    /// </summary>
    public void SetFoodLocation(FoodLocation foodLocation) => this.foodLocation = foodLocation;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject != cuttingBoard || onCuttingBoard)
            return;

        onCuttingBoard = true;
        cuttingManager.AddItemCut(this);
    }

    /// <summary>
    /// Changes the color of the food to be yellowish.
    /// </summary>
    private void ChangeColorTo(Color currentColor, Color targetColor, float percent)
    {
        if (materialRenderer == null)
            return;

        Color lerpedColor = Color.Lerp(currentColor, targetColor, percent);
        materialRenderer.material.color = lerpedColor;
    }
}