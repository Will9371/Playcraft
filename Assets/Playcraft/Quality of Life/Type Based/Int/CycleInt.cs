using UnityEngine;

// Separate into mono and delegate variations
namespace Playcraft
{
    public class CycleInt : MonoBehaviour
    {
        [SerializeField] int value;
        [SerializeField] int min;
        [SerializeField] int max;
        [SerializeField] bool reverse;
        [SerializeField] IntEvent Output;
        
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
