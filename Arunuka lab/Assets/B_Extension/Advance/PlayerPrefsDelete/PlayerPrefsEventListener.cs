using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class PlayerPrefsEventListener : MonoBehaviour
{
    [SerializeField] protected bool onAwake;
    [SerializeField] protected bool onEnable;
    [SerializeField] protected bool periodic;
    [SerializeField] protected string key;
    [SerializeField] protected UnityEvent onExistKey;
    [SerializeField] protected UnityEvent onDoesntExistKey;
    public event System.Action<bool> OnExistKey;

    protected void OnEnable()
    {
        if (onEnable)
        {
            CheckExist();
        }

        if (periodic)
            StartCoroutine(DoPeriodicCheck());
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected void Awake()
    {
        if (onAwake)
        {
            CheckExist();
        }
    }

    private IEnumerator DoPeriodicCheck() 
    {
        while (periodic) 
        {
            yield return new WaitForSecondsRealtime(1f);
            CheckExist();
        }
    }
    public void CheckExist()
    {
        if (PlayerPrefs.HasKey(key))
        {
            OnExistKey?.Invoke(true);
            onExistKey?.Invoke();
        }
        else
            onExistKey?.Invoke();
    }
}
