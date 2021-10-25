using UnityEngine;
using UnityEngine.Events;
using System.Linq;

public class OnRaycastHitEventTrigger : MonoBehaviour
{
    public Transform origin;
    public float maxDistance = 3f;
    public LayerMask layerMask;
    public string[] tags;
    public UnityEvent<RaycastHit> onRaycastHit;

    public void PerformRaycast()
    {
        Ray ray = new Ray(origin.position, origin.forward);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, maxDistance, layerMask))
        {
            if (tags.Length == 0)
                onRaycastHit.Invoke(hitInfo);
            else if (tags.Any(hitInfo.collider.tag.Contains))
                onRaycastHit.Invoke(hitInfo);
        }
    }
}
