using UnityEngine;
using UnityEngine.Events;

public class PeriodicEventTrigger : MonoBehaviour
{
    public float startPeriod = 1f;
    public float startChance = 0.5f;
    public float startPeriodDrift = 0.5f;
    public UnityEvent onTrigger;

    public float period { get; set; }
    public float chance { get; set; }
    public float periodDrift { get; set; }

    private float elapsed = 0f;
    private float currentPeriod;

    private void Awake()
    {
        period = startPeriod;
        chance = startChance;
        periodDrift = startPeriodDrift;
        ResetPeriod();
    }


    private void Update()
    {
        elapsed += Time.deltaTime;
        if (elapsed >= currentPeriod)
        {
            ResetPeriod();
            if (Random.value < chance)
            {
                onTrigger.Invoke();
            }
        }
    }

    public void ResetPeriod()
    {
        elapsed = 0f;
        currentPeriod = period + (Random.value * 2f - 1f) * period * periodDrift;
    }
}
