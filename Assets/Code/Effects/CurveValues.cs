using UnityEngine;
using UnityEngine.Events;

public class CurveValues : MonoBehaviour
{
    public AnimationCurve values;
    public UnityEvent<float> onNewValue;

    /// <summary>
    /// Invoke event with a value picked at random along the curve.
    /// </summary>
    public void RandomValue()
    {
        onNewValue.Invoke(values.Evaluate(Random.value));
    }
}
