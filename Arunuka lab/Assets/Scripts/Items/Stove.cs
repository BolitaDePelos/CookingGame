using UnityEngine;
using UnityEngine.Events;

public class Stove : MonoBehaviour, IUsable
{
    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }

    [SerializeField] UnityEvent OnStoveOn;
    [SerializeField] UnityEvent OnStoveOff;

    [Header("Collider")]
    public GameObject colliderFlame;

    [Header("VFX")]
    public GameObject vfx;

    [Header("Animation")]
    [SerializeField] public NameAnimation animator;

    [SerializeField] private string Open = "Open";
    [SerializeField] private string Close = "Close";
    public bool IsActivate;

    AudioManager audioManager;

    private void Start()
    {
        audioManager = AudioManager.Instance;
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
            OnStoveOn?.Invoke();
            audioManager.PlaySoundTurnOnStove();
        }
        else
        {
            animator.PlayAnimationByName(Close);
            vfx.SetActive(false);
            colliderFlame.SetActive(false);
            OnStoveOff?.Invoke();
        }
    }
}