using UnityEngine;

namespace KhaoTom.AdaptiveMusic
{
    [RequireComponent(typeof(SphereCollider))]
    public class SphereTempoInfluencer : TempoInfluencer
    {
        protected override float CalculateInfluence(Vector3 otherPosition)
        {
            var collider = (SphereCollider)triggerCollider;
            return (collider.radius - Vector3.Distance(transform.position, otherPosition)) / collider.radius;
        }
    }
}
