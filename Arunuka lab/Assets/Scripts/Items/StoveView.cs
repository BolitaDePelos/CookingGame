using UnityEngine;
using UnityEngine.Events;

public class StoveView : MonoBehaviour, IUsable
{
    [field: SerializeField]
    public UnityEvent OnUse { get; private set; }

    [SerializeField] private Player player;

    private void OnEnable()
    {
        GameEventsManager.Instance.InputEvents.OnDropPressed += Desactivate;
    }

    private void OnDisable()
    {
        GameEventsManager.Instance.InputEvents.OnDropPressed -= Desactivate;
    }

    public void Use(GameObject actor)
    {
        OnUse?.Invoke();
    }
    
    public void Desactivate()
    {
        player.GetComponentInParent<Animator>().Play("Default");
    }
}