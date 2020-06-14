using UnityEngine;

namespace Playcraft
{
    public class FilterByCollisionAngle : MonoBehaviour
    {
        [SerializeField] Vector3 referenceDirection = Vector3.up;
        [SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
        [SerializeField] [Range(-1f, 1f)] float minDot = .5f;
        [SerializeField] CollisionEvent OnSuccess;
        
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
            //Debug.Log(other.contacts[0].normal);
            var direction = other.contacts[0].normal;
            var dot = Vector3.Dot(direction, referenceDirection);
            
            if (dot >= minDot)
                OnSuccess.Invoke(other);
        }
    }
}
