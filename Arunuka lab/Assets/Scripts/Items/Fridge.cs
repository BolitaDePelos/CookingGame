
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
    public bool isOpen;
    public int tutorialFridgeTask=2;
    public bool tutorialMode;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
        OnUse.AddListener(ToggleFridgeState);
        OnUse.AddListener(PlayAnimation);
    }

    //private void Update()
    //{
    //    ToggleFridgeState();
    //}

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
        isOpen = !isOpen;
    }


    public void PlayAnimation()
    {
        if (isOpen)
        {
            animator.PlayAnimationByName(Open);
            audioManager.PlayFridgeOpenSound();
            playerAnimator.SetTrigger("Default");
        }
        else
        {
            animator.PlayAnimationByName(Close);
            audioManager.PlayFridgeCloseSound();
            playerAnimator.SetTrigger("Fridge");
        }
    }

}
