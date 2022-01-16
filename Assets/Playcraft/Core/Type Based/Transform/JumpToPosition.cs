using UnityEngine;

namespace Playcraft
{
    public class JumpToPosition : MonoBehaviour
    {
        public void SetPosition(Transform value) { SetPosition(value.position); }
        public void SetPosition(RaycastHit value) { SetPosition(value.point); }
        public void SetPosition(Vector3 value) { transform.position = value; }
        
        public void SetAtTransformPosition(RaycastHit hit) { SetPosition(hit.transform.position); }
        public void SetAtTransformInterfacePosition(RaycastHit hit)
        {
            var access = hit.transform.GetComponent<TransformInterface>();
            if (!access) return;
            SetPosition(access.position);
        }
        
        public void SetLocalPosition(Vector3 value) { transform.localPosition = value; }
    }
}