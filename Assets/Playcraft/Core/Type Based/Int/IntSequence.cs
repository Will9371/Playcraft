using UnityEngine;
using UnityEngine.Events;

// INPUT: numbers
// PROCESS: check if input number is current value in sequence
// OUTPUT: incremented number, cycle event
namespace Playcraft
{
    public class IntSequence : MonoBehaviour
    {
        [SerializeField] int startingIndex;
        [SerializeField] int[] sequence;
        [SerializeField] IntEvent Output;
        [SerializeField] UnityEvent OnCycle;
        [SerializeField] bool outputOnStart;
        
        int current => sequence[index];
        
        int index;
        
        void Start()
        {
            index = startingIndex;
            
            if (outputOnStart)
                Broadcast();
        }
        
        public void Input(int value)
        {
            if (value != current) 
                return;
            
            index++;
            if (index >= sequence.Length) 
            {
                index = 0;
                OnCycle.Invoke();
            }
            
            Broadcast();
        }
        
        public void Broadcast() { Output.Invoke(current); }
    }
}
