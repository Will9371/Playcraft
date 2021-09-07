using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class PlaceAtTag : MonoBehaviour
    {
        [SerializeField] Vector3 raycastSourceOffset = new Vector3(0, 1, 0);
        [SerializeField] Vector3 raycastVector = new Vector3(0, -2, 0);
        [SerializeField] Vector3 placementOffset = new Vector3(0, .01f, 0f);
        [SerializeField] SO[] validTags; 
        
        Vector3 raycastSourcePoint => transform.position + raycastSourceOffset;

        public void Trigger()
        {
            var ray = new Ray(raycastSourcePoint, raycastVector);
            var hits = Physics.RaycastAll(ray);     // * use NonAlloc
            var hitPoints = GetValidPoints(hits);
            var firstHitPoint = VectorMath.GetClosestPoint(hitPoints, raycastSourcePoint, transform.position);
            
            if (firstHitPoint != transform.position)
                transform.position = firstHitPoint + placementOffset;
        }
        
        List<Vector3> GetValidPoints(RaycastHit[] hits)
        {
            var validPoints = new List<Vector3>();
            
            foreach (var hit in hits)
            {
                if (hit.collider == null) continue;
                
                var tags = hit.collider.GetComponent<CustomTags>();
                if (!tags || !tags.HasAnyTag(validTags)) continue;
                
                validPoints.Add(hit.point);
            }
            
            return validPoints;
        }
    }
}
