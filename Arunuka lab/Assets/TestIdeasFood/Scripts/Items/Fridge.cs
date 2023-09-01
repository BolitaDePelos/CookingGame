
using UnityEngine;
using UnityEngine.Events;

public class Fridge : MonoBehaviour, IUsable
{
    [field:SerializeField]
    public UnityEvent OnUse { get; private set; }

    [SerializeField] public NameAnimation animator;
    [SerializeField] string Open = "Open";
    [SerializeField] string Close = "Close";
    public bool isOpen;

    public void Use(GameObject actor)
    {
        ToggleFridgeState();
        PlayAppropriateAnimation();
        OnUse?.Invoke();

    }

    public void ToggleFridgeState()
    {
        isOpen = !isOpen;
    }

    private void PlayAppropriateAnimation()
    {
        if (isOpen)
        {
            animator.PlayAnimationByName(Open);
        }
        else
        {
            animator.PlayAnimationByName(Close);
        }
    }

}
