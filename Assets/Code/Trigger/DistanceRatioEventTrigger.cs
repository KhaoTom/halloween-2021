using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Evaluate the current ratio of distance / max distance - previous ratio.
/// </summary>
public class DistanceRatioEventTrigger : MonoBehaviour
{
    public Transform transform1;
    public Transform transform2;
    public float maxDistance = 10f;

    public UnityEvent<float> onNewValue;

    private float previousValue = 0f;
    public void EvaluateDifferenceFromPreviousValue()
    {
        var distance = Vector3.Distance(transform1.position, transform2.position);
        var ratio = distance / maxDistance;
        var difference = ratio - previousValue;
        previousValue = ratio;
        Debug.Log($"{distance}, {ratio}, {difference}");
        onNewValue.Invoke(difference);
    }
}
