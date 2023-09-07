
using UnityEngine;
using UnityEngine.Events;

public class Fridge : MonoBehaviour, IUsable
{
    [field:SerializeField]
    public UnityEvent OnUse { get; private set; }
    
    [Header("Animation")]
    [SerializeField] public NameAnimation animator;
    [SerializeField] string Open = "Open";
    [SerializeField] string Close = "Close";
    public bool isOpen;

    private void Update()
    {
        ToggleFridgeState();
    }

    public void Use(GameObject actor)
    {
        OnUse?.Invoke();

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
        }
        else
        {
            animator.PlayAnimationByName(Close);
        }
    }

}
