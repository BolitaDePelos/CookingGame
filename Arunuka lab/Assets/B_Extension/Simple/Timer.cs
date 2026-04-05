using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float targetTime = 0;
    float timeRemaing = 0;
    [SerializeField] private bool reduce = false;
    public event Action<string> OnUpdateTime;
    public event Action OnTimeCompleted;
    Coroutine coroutineTimer;
    [SerializeField] private UnityEvent onStartTimer;
    [SerializeField] private UnityEvent onStopTimer;
    [SerializeField] private UnityEvent onTimeCompleted;

    public void StopTimer()
    {
        if(coroutineTimer != null)
            StopCoroutine(coroutineTimer);
        onStopTimer?.Invoke();
    }

    public void StartTimer() 
    {
        if (reduce)
            timeRemaing = targetTime;
        else
            timeRemaing = 0;

        if (coroutineTimer == null)
            coroutineTimer = StartCoroutine(DoTimer());
        onStartTimer?.Invoke();
    }

    public void RestartTimer() 
    {
        coroutineTimer = null;
        StartTimer();
    }

    private IEnumerator DoTimer()
    {
        float amount = (reduce) ? -1 : 1;

        while (!ReachTime())
        { 
            var secs = TimeSpan.FromSeconds(timeRemaing);
            timeRemaing += (amount *Time.deltaTime);
            OnUpdateTime?.Invoke(secs.ToString("mm\\:ss"));
            // return null is better cause wait til next frame
            yield return null;
        }
        coroutineTimer = null;
        onTimeCompleted?.Invoke();
        OnTimeCompleted?.Invoke();
    }

    public bool ReachTime() 
    {
        if(reduce)
            return timeRemaing <= 0;
        else
            return timeRemaing >= targetTime;
    }
}


