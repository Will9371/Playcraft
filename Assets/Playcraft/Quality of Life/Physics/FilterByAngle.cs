using UnityEngine;

namespace Playcraft
{
    public class FilterByAngle : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector3 referenceDirection = Vector3.up;
        [Tooltip("Leave as null to use world space")]
        [SerializeField] Transform referenceTransform;
        [SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
        [SerializeField] [Range(-1f, 1f)] float minDot = .5f;
        [SerializeField] CollisionEvent OutputCollisionOnSuccess;
        [SerializeField] BoolEvent OutputResult;
        #pragma warning restore 0649
        
        private float priorMaxAngle;
        private float priorMinDot;
        
        private void OnValidate()
        {
            if (priorMaxAngle != maxAngle)
                minDot = VectorMath.AngleToDot(maxAngle);
            if (priorMinDot != minDot)
                maxAngle = VectorMath.DotToAngle(minDot);
            
            priorMaxAngle = maxAngle;
            priorMinDot = minDot;
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
