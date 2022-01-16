using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class CycleInt
    {
        [SerializeField] int value;
        [SerializeField] int min;
        [SerializeField] int max;
        [SerializeField] bool reverse;
            
        public int Cycle()
        {
            value = reverse ? value - 1 : value + 1;
                
            if (!reverse && value > max)
                value = 0;
            else if (reverse && value < min)
                value = max;
                    
            return value;
        } 
            
        public void SetValue(int value) { this.value = value; }
        public void SetReverse(bool value) { reverse = value; }
    }
}
