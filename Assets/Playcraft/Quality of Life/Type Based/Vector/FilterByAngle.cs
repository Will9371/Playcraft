using System.Collections.Generic;
using UnityEngine;

// Use DrawDebugLinesFromPoint for visualization
namespace Playcraft
{
    public class FilterByAngle : MonoBehaviour
    {
        [SerializeField] Transform source;
        [SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
        [SerializeField] ColliderListEvent Output;
        
        ValidateAngleToDot angleDot;
        
        Filter_By_Angle _process;
        Filter_By_Angle process
        {
            get
            {
                if (_process == null)
                    _process = new Filter_By_Angle(source, maxAngle);
                    
                return _process;
            }
        }
                
        void OnValidate() { SetMaxAngle(maxAngle); }
        
        public void SetMaxAngle(float value)
        {
            maxAngle = value;
            process.SetMaxAngle(maxAngle);           
        }
                                
        public void Input(List<Collider> values) { Output.Invoke(process.Input(values)); }
    }
    
    public class Filter_By_Angle
    {
        readonly Transform source;
        float minDot = .5f;
    
        ValidateAngleToDot angleDot;
        
        public Filter_By_Angle(Transform source, float maxAngle)
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
