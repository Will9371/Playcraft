using UnityEngine;

namespace Playcraft
{
    public class FollowOffset : MonoBehaviour
    {
        [SerializeField] Transform target;
        public void SetTarget(Transform value) { target = value; }
        
        [SerializeField] Vector3 offset;
        public void SetOffset(Vector3 value) { offset = value; }
        
        [SerializeField] Vector3 lookatOffset;
        public void SetLookatOffset(Vector3 value) { lookatOffset = value; }

        void Update()
        {
            transform.position = target.position + offset;
            transform.LookAt(target.position + lookatOffset);
        }
    }
}
