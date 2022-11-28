using UnityEngine;

// REFACTOR: variation too broad, split into separate components -> physical, non-physical
// Or RENAME for more specific purpose -> remove from Character cameras, specialize on pickups
namespace ZMD
{
    public class FollowInstant : MonoBehaviour
    {
        enum OffsetType { None, Simple, Pivot }
        
        public bool active;
        public Transform target;
    
        [SerializeField] new RBInterface rigidbody;
        [SerializeField] GetVelocity speedometer;
        [SerializeField] OffsetType offsetType;
        [SerializeField] bool matchTargetRotation = true;
        [SerializeField] bool throwOnRelease;
            
        public void SetTarget(GameObject value) 
        {
            if (value == null) ClearTarget(); 
            else SetTarget(value.transform); 
        }
        public void SetTarget(Transform value) 
        {
            if (lockTarget || value == target) return;
            target = value; 
            otherBeginRotation = target.rotation;
        }
        
        public void ClearTarget() { target = null; }
        
        bool lockTarget;
        public void SetLockTarget(bool value) { lockTarget = value; }
        
        Vector3 offsetPosition;
        public void SetOffsetPosition(Vector3 value) { offsetPosition = value; }
        
        Quaternion otherBeginRotation;
        Quaternion targetRotationDelta => target.rotation * Quaternion.Inverse(otherBeginRotation);
        
        public void Follow(bool value)
        {
            active = value;
            if (active) offsetPosition = transform.position - target.position;
        }
        
        public void PhysicalFollow(bool value)
        {
            if (!rigidbody || value && target == null) return;
            
            Follow(value);
            rigidbody.SetPhysicsActive(!value);
            
            if (!value && throwOnRelease)
                rigidbody.SetVelocity(speedometer.Velocity, speedometer.AngularVelocity);
        }
        
        void LateUpdate()
        {
            if (!target || !active) return;
            transform.position = target.position + GetOffset();
            
            if (matchTargetRotation) 
                transform.rotation = target.rotation;    
        }
        
        Vector3 GetOffset()
        {
            switch (offsetType)
            {
                case OffsetType.None: return Vector3.zero;
                case OffsetType.Simple: return offsetPosition;
                case OffsetType.Pivot: return targetRotationDelta * offsetPosition;
                default: return Vector3.zero;
            }
        }
    }
}
