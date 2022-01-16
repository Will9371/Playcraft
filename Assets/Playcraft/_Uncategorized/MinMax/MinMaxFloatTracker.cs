using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class MinMaxFloatTracker
    {
        public Vector2 range;
        
        public float max => range.y;
        public float min => range.x;
        
        float _value;
        public float value
        {
            get => _value;
            set => _value = Mathf.Clamp(value, min, max);
        }
        
        public void ResetMax(float value) 
        { 
            range.y = value; 
            SetToMax();
        } 
        
        public void SetToMax() { value = max; }
        public void SetToMin() { value = min; }
        
        public (float, bool) Subtract(float value) 
        { 
            this.value -= value; 
            return (this.value, this.value <= min);
        }
        
        public (float, bool) Add(float value) 
        { 
            this.value += value; 
            return (this.value, this.value >= max);
        }
    }
}
