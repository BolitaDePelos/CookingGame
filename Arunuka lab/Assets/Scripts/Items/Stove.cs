using UnityEngine;
using UnityEngine.Events;

public class Stove : MonoBehaviour, IUsable
{
    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }

    [Header("Collider")]
    public GameObject colliderFlame;

    [Header("VFX")]
    public GameObject vfx;

    [Header("Animation")]
    [SerializeField] public NameAnimation animator;

    [SerializeField] private string Open = "Open";
    [SerializeField] private string Close = "Close";
    public bool IsActivate;

    private void Start()
    {
        colliderFlame.SetActive(false);
        vfx.SetActive(false);
    }

    private void Update()
    {
        ToggleStoveState();
    }

    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
    }

    public void ToggleStoveState()
    {
        IsActivate = !IsActivate;
    }

    public void Active()
    {
        if (IsActivate)
        {
            animator.PlayAnimationByName(Open);
            vfx.SetActive(true);
            colliderFlame.SetActive(true);
            Pot.Instance.SetStoveActive(true);
        }
        else
        {
            animator.PlayAnimationByName(Close);
            vfx.SetActive(false);
            colliderFlame.SetActive(false);
            Pot.Instance.SetStoveActive(false);
        }
    }
}