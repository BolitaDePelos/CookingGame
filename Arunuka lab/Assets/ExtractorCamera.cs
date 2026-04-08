using UnityEngine;
using UnityEngine.Events;

public class ExtractorCamera : MonoBehaviour,IUsable,IPickable
{
    [field: SerializeField] public UnityEvent OnDrop { get; private set; }
    [field:SerializeField] public UnityEvent OnUse { get; private set; }

    public bool KeepWorldPosition => throw new System.NotImplementedException();

    public void Drop()
    {
        OnDrop?.Invoke();
    }

    public bool IsPickable()
    {
        Use(gameObject);
        return true;
    }

    public bool IsPickedUp()
    {
        throw new System.NotImplementedException();
    }

    public GameObject PickUp(GameObject picker)
    {
        print("ExtractorCamera picked up by " + picker.name);
        return null;
    }

    public void SetIsPickable(bool isPickable)
    {
        throw new System.NotImplementedException();
    }

    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
    }
}
