using UnityEngine;

// REFACTOR: variation too broad, split into separate components -> physical, non-physical
namespace Playcraft
{
    public class FollowInstant : MonoBehaviour
    {
        enum OffsetType { None, Simple, Pivot }
        
        public bool active;
        public Transform target;
    
        #pragma warning disable 0649
        [SerializeField] new RBInterface rigidbody;
        [SerializeField] GetVelocity speedometer;
        [SerializeField] OffsetType offsetType;
        [SerializeField] bool matchTargetRotation = true;
        [SerializeField] bool throwOnRelease;
        #pragma warning restore 0649
            
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
        
        //public void SetTarget(MessageLink link)
        //{
        //    if (link == null) ClearTarget();
        //    else SetTarget(link.transform);
        //}
        
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

// Quaternion selfRotationOffset;
// From SetTarget: selfRotationOffset = Quaternion.Inverse(otherBeginRotation) * transform.rotation;

// Best: jumps on begin to face target consistently, otherwise OK
//Quaternion selfRotation => target.rotation; 
//Quaternion selfRotation => target.rotation * Quaternion.Inverse(Quaternion.identity);   // Same   
        
// OK, jumps to face forward on begin
//Quaternion selfRotation => target.rotation * Quaternion.Inverse(selfBeginRotation); 
//Quaternion selfRotation => target.rotation * selfBeginRotation;    // jumps based on start


// NG
//Quaternion selfRotation => selfBeginRotation * targetRotationDelta;    // Expected
//Quaternion selfRotation => selfBeginRotation * Quaternion.Inverse(targetRotationDelta);
//Quaternion selfRotation => selfBeginRotation * target.rotation;
//Quaternion selfRotation => selfBeginRotation * Quaternion.Inverse(target.rotation);
//Quaternion selfRotation => targetRotationDelta;
// transform.rotation = Quaternion.Inverse(otherBeginRotation) * target.rotation;
