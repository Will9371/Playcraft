using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FilterByAngle
    {
        readonly Transform source;
        float minDot = .5f;
        
        ValidateAngleToDot angleDot;
            
        public FilterByAngle(Transform source, float maxAngle)
        {
            this.source = source;
            SetMaxAngle(maxAngle);
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