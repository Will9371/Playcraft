using UnityEngine;

namespace Playcraft
{
    public class CycleInt : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] int value;
        [SerializeField] int min;
        [SerializeField] int max;
        [SerializeField] bool reverse;
        [SerializeField] IntEvent Output;
        #pragma warning restore 0649
        
        public void Cycle()
        {
            value = reverse ? value - 1 : value + 1;
            
            if (!reverse && value > max)
                value = 0;
            else if (reverse && value < min)
                value = max;
                
            Output.Invoke(value);
        } 
        
        public void SetValue(int value) { this.value = value; }
        public void SetReverse(bool value) { reverse = value; }
    }
}
