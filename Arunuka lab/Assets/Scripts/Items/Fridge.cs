
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Fridge : MonoBehaviour, IUsable
{
    [field:SerializeField] public UnityEvent OnUse { get; private set; }
    [SerializeField] private Animator playerAnimator;

    [Header("Animation")]
    [SerializeField] public NameAnimation animator;
    [SerializeField] string Open = "Open";
    [SerializeField] string Close = "Close";
    public static bool IsOpen = false;
    public int tutorialFridgeTask=2;
    public bool tutorialMode;
    private AudioManager audioManager;
    // wait animation
    private bool computing;
    private bool collected;
    private void Start()
    {
        audioManager = AudioManager.Instance;
        OnUse.AddListener(ToggleFridgeState);
    }

    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
        if (tutorialMode&&TutorialManager.Instance.index==tutorialFridgeTask) {
            TutorialManager.Instance.NextText();
            tutorialMode = false;
        }
    }

    public void ToggleFridgeState()
    {
        if (computing)
            return;
        if (collected)
        {
            // Just play the animation without doing anything else, since the food is already collected and the fridge state is already changed
            IsOpen = !IsOpen;
            PlayAnimation();
            return;
        }
        computing = true;

        if (IsOpen)
        {
            // collect the food and close the fridge after 2 seconds, which is the time of the animation, to avoid collecting the food before the animation ends
            Invoke(nameof(PlayAnimation), 2.0f);
            IsOpen = !IsOpen;
            collected = true;
            RecipeManager.Instance.currentRecipe.expectedFoodResult.ForEach(expectedFoodResult =>
            {
                Food food = FridgeWrapper.Instance.GetIngredient(expectedFoodResult.ingredient);
                TableManager.Instance.ConfigureFoodKitchenSector(food);
            });

            return;
        }
        IsOpen = !IsOpen;
        PlayAnimation();
    }

    public void PlayAnimation()
    {
        if (IsOpen)
        {
            animator.PlayAnimationByName(Open);
            audioManager.PlayFridgeOpenSound();
        }
        else
        {
            animator.PlayAnimationByName(Close);
            audioManager.PlayFridgeCloseSound();
        }
        playerAnimator.SetBool("Fridge", IsOpen);
        computing = false;
    }
}