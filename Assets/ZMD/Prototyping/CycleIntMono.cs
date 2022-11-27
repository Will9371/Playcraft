using System;
using UnityEngine;

namespace ZMD
{
    public class CycleIntMono : MonoBehaviour
    {
        [SerializeField] CycleInt process;
        [SerializeField] IntEvent Output;
        
        public void Cycle() { Output.Invoke(process.Cycle()); }
        public void SetValue(int value) { process.SetValue(value); }
        public void SetReverse(bool value) { process.SetReverse(value); }
    }
    
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
