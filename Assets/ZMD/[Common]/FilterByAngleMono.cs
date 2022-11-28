using System;
using System.Collections.Generic;
using UnityEngine;

// Use DrawDebugLinesFromPoint for visualization
namespace ZMD
{
    public class FilterByAngleMono : MonoBehaviour
    {
        [SerializeField] ColliderListEvent Output;
        
        ValidateAngleToDot angleDot;
        FilterByAngle process;

        void OnValidate() { process.Validate(); }
        public void SetMaxAngle(float value) { process.SetMaxAngle(value); }
        public void Input(List<Collider> values) { Output.Invoke(process.Input(values)); }
    }
    
    [Serializable]
    public class FilterByAngle
    {
        [SerializeField] Transform source;
        [SerializeField] [Range(0f, 360f)] float _maxAngle;
        
        ValidateAngleToDot angleDot;
        float minDot = .5f;
        
        public void Validate()
        {
            SetMaxAngle(_maxAngle);
        }

        public void SetMaxAngle(float maxAngle)
        {
            if (angleDot == null) angleDot = new ValidateAngleToDot();
            minDot = angleDot.AngleToDot(maxAngle, minDot);
            angleDot.DotToAngle(maxAngle, minDot);
        }
            
        List<Collider> withinRange = new();
            
        Vector3 _targetDirection;
        float _dot;
                    
        public List<Collider> Input(List<Collider> values)
        {            
            withinRange.Clear();

            foreach (var value in values)
            {
                _targetDirection = (value.transform.position - source.position).normalized;
                _dot = Vector3.Dot(source.forward, _targetDirection);
                if (_dot >= minDot) withinRange.Add(value);
            }
                
            return withinRange;            
        }
    }
}
