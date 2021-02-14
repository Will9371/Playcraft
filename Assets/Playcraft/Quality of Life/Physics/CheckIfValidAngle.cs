using UnityEngine;

namespace Playcraft
{
    public class CheckIfValidAngle : MonoBehaviour
    {
        [SerializeField] Transform referenceTransform;
        [SerializeField] Vector3 referenceDirection = Vector3.up;
        [SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
        [SerializeField] [Range(-1f, 1f)] float minDot = .5f;
        [Tooltip("Leave as null to use world space")]
        [SerializeField] CollisionEvent OutputCollisionOnSuccess;
        [SerializeField] BoolEvent OutputResult;
        
        ValidateAngleToDot angleDot;
        
        void OnValidate()
        {
            if (angleDot == null) angleDot = new ValidateAngleToDot();
            minDot = angleDot.AngleToDot(maxAngle, minDot);
            maxAngle = angleDot.DotToAngle(maxAngle, minDot);
        }
        
        public void Input(Collision other)
        {
            if (Input(other.contacts[0].normal))
                OutputCollisionOnSuccess.Invoke(other);
        }
        
        public bool Input(Vector3 direction)
        {
            var localReferenceDirection = referenceDirection;
            if (referenceTransform != null)
                localReferenceDirection = referenceTransform.TransformVector(referenceDirection);
            
            var dot = Vector3.Dot(direction, localReferenceDirection);
            var result = dot >= minDot;
            OutputResult.Invoke(result);
            return result;
        }
    }
}
