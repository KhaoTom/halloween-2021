using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Player proximity increases influence.
/// Use physics layers to limit and manage what goes into inTrigger list.
/// </summary>
public abstract class TempoInfluencer : MonoBehaviour
{
    public List<Transform> inTrigger = new List<Transform>();

    public int initialBpmOffset = 90;
    public Collider triggerCollider;

    public int bpmOffset { get; set; }
    public float influence { get => GetInfluence(); }

    private void OnTriggerEnter(Collider other)
    {
        inTrigger.Add(other.transform);
    }

    private void OnTriggerExit(Collider other)
    {
        inTrigger.Remove(other.transform);
    }

    private void Awake()
    {
        bpmOffset = initialBpmOffset;
        if (triggerCollider == null) triggerCollider = GetComponentInChildren<Collider>();
    }

    public float GetInfluence()
    {
        if (inTrigger.Count > 0)
            return CalculateInfluence(inTrigger[0].position);
        else
            return 0f;
    }

    protected abstract float CalculateInfluence(Vector3 otherPosition);
}
