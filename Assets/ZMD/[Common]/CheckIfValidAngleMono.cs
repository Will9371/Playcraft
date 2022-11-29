using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class CheckIfValidAngleMono : MonoBehaviour
    {
        public CheckIfValidAngle process;
        
        [Tooltip("Leave as null to use world space")]
        [SerializeField] UnityEvent OutputCollisionOnSuccess;
        
        ValidateAngleToDot angleDot;
        
        void OnValidate() { process.OnValidate(); }

        public void Input(Collision other) 
        {
            if (process.Input(other))
                OutputCollisionOnSuccess.Invoke();
        }
    }
    
    [Serializable]
    public class CheckIfValidAngle
    {
        public Transform referenceTransform;
        public Vector3 referenceDirection = Vector3.up;
        [Range(0f, 360f)] public float maxAngle = 45f;
        [Range(-1f, 1f)] public float minDot = .5f;

        ValidateAngleToDot angleDot;
        
        public void OnValidate()
        {
            if (angleDot == null) angleDot = new ValidateAngleToDot();
            minDot = angleDot.AngleToDot(maxAngle, minDot);
            maxAngle = angleDot.DotToAngle(maxAngle, minDot);
        }
        
        public bool Input(Collision other) { return Input(other.contacts[0].normal); }
        
        public bool Input(Vector3 direction)
        {
            var localReferenceDirection = referenceDirection;
            if (referenceTransform != null)
                localReferenceDirection = referenceTransform.TransformVector(referenceDirection);
            
            var dot = Vector3.Dot(direction, localReferenceDirection);
            var result = dot >= minDot;
            //Debug.Log($"direction: {direction}, reference direction: {referenceDirection}, dot {dot}, result {result}");
            return result;
        }        
    }
}
