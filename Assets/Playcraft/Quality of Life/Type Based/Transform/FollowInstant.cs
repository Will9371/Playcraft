using UnityEngine;

namespace Playcraft
{
    public class FollowInstant : MonoBehaviour
    {
        [SerializeField] bool pivot = true;
        [SerializeField] bool matchRotation = true;
    
        public Transform target;
        public void SetTarget(GameObject value) { SetTarget(value.transform); }
        public void SetTarget(Transform value) 
        { 
            target = value; 
            otherBeginRotation = target.rotation;
            //selfRotationOffset = Quaternion.Inverse(otherBeginRotation) * transform.rotation;
        }
        public void ClearTarget() { target = null; }
        
        Vector3 offsetPosition;
        public void SetOffsetPosition(Vector3 value) { offsetPosition = value; }
        
        //Quaternion selfRotationOffset;
        Quaternion otherBeginRotation;
        
        Vector3 orbit => targetRotationDelta * offsetPosition; 
        Vector3 offset => pivot ? orbit : offsetPosition;
        Quaternion targetRotationDelta => target.rotation * Quaternion.Inverse(otherBeginRotation);
        
        void LateUpdate()
        {
            if (!target) return;
            transform.position = target.position + offset;
            
            if (matchRotation) 
            {
                transform.rotation = target.rotation;    // TBD: keep original rotation
            }
        }
    }
}

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
