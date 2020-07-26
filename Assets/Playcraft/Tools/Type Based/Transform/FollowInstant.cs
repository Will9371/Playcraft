using UnityEngine;

namespace Playcraft
{
    public class FollowInstant : MonoBehaviour
    {
        [SerializeField] bool pivot = true;
        [SerializeField] bool matchRotation = true;
    
        public Transform target;
        public void SetTarget(Transform value) { target = value; }
        public void SetTarget(GameObject value) { target = value.transform; }
        public void ClearTarget() { target = null; }
        
        Vector3 offsetPosition;
        public void SetOffsetPosition(Vector3 value) { offsetPosition = value; }
        
        Vector3 orbit => target.rotation * offsetPosition;
        Vector3 offset => pivot ? orbit : offsetPosition;

        void LateUpdate()
        {
            if (!target) return;
            transform.position = target.position + offset;
            if (matchRotation) transform.rotation = target.rotation;
        }
    }
}
