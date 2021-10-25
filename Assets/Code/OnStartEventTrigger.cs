using UnityEngine;
using UnityEngine.Events;

public class OnStartEventTrigger : MonoBehaviour
{
    public UnityEvent onStart;

    private void Start()
    {
        onStart.Invoke();
    }
}
