using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PulseEventTrigger : MonoBehaviour
{
    public float startDuration = 1f;
    public bool playOnAwake = false;
    public UnityEvent onPulseStart;
    public UnityEvent onPulseEnd;
    public UnityEvent<float> onPulseTime;

    private float _duration;
    public float duration { get => _duration; set => _duration = Mathf.Max(0.01f, value); }

    private float elapsed;

    private void Awake()
    {
        duration = startDuration;
        this.enabled = playOnAwake;
    }

    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= duration)
        {
            elapsed = 0f;
            onPulseEnd.Invoke();
            this.enabled = false;
        }
        else
        {
            onPulseTime.Invoke(elapsed / duration);
        }
    }

    public void StartPulse()
    {
        this.enabled = true;
        onPulseStart.Invoke();
    }
}
