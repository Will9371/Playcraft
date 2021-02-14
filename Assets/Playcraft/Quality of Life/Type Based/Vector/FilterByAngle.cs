using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FilterByAngle : MonoBehaviour
    {
        [SerializeField] Transform referenceTransform;
        [SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
        [SerializeField] [Range(-1f, 1f)] float minDot = .5f;
        
        ValidateAngleToDot angleDot;
        
        void OnValidate()
        {
            if (angleDot == null) angleDot = new ValidateAngleToDot();
            minDot = angleDot.AngleToDot(maxAngle, minDot);
            maxAngle = angleDot.DotToAngle(maxAngle, minDot);
        }
        
        [SerializeField] ColliderListEvent Output;
        
        [SerializeField] Color debugLineColor;
        [SerializeField] float debugLineLength;
        [SerializeField] Vector3[] debugLineAxes;
        
        List<Collider> withinRange = new List<Collider>();
        Vector3 _targetDirection;
        float _dot;
        
        public void Input(List<Collider> values)
        {
            if (!referenceTransform) return;
            
            withinRange.Clear();

            foreach (var value in values)
            {
                _targetDirection = (value.transform.position - referenceTransform.position).normalized;
                _dot = Vector3.Dot(referenceTransform.forward, _targetDirection);
                if (_dot >= minDot) withinRange.Add(value);
            }
            
            Output.Invoke(withinRange);
        }
        
        void OnDrawGizmos()
        {
            Gizmos.color = debugLineColor;
            foreach (var axis in debugLineAxes)
            {
                DrawRay(maxAngle/2f, axis); 
                DrawRay(-maxAngle/2f, axis);     
            } 
        }
        
        void DrawRay(float angle, Vector3 axis)
        {
            var rotation = Quaternion.AngleAxis(angle, axis);
            var localDirection = rotation * referenceTransform.forward;
            Gizmos.DrawRay(referenceTransform.position, localDirection * debugLineLength);            
        }
    }
}
