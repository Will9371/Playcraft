using System;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
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
            
        List<Collider> withinRange = new List<Collider>();
            
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