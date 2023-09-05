
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class Stove : MonoBehaviour, IUsable
{

    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }
    public GameObject StoveCollider;
    public bool IsActivate;

    private void Start()
    {
        StoveCollider.SetActive(false);
    }

    public void Use(GameObject actor)
    {
        ActiveCollider();
        ToggleStoveState();
        OnUse?.Invoke();

    }

    
    public void ToggleStoveState()
    {
        IsActivate = !IsActivate;
    }

    private void ActiveCollider()
    {
        if (IsActivate)
        {
            StoveCollider.SetActive(true);
        }
        else
        {
            StoveCollider.SetActive(false);
        }
    }

}
