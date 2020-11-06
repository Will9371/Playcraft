using UnityEngine;

namespace Playcraft.FPS
{
    public class LookAtTarget : MonoBehaviour
    {
        [SerializeField] Transform defaultRotationReference = default;

        public void Aim(RaycastHit hit)
        {
            if (IsValidTarget(hit.collider))
                transform.LookAt(hit.point);
            else if (Vector3.Distance(hit.point, transform.position) < .5f) 
                transform.rotation = defaultRotationReference.rotation; 
            else
                transform.rotation = defaultRotationReference.rotation;                
        }
        
        bool IsValidTarget(Collider other)
        {
            if (other == null) return false;
            Target target = other.GetComponent<Target>();
            return target != null;      
        }
    }
}
