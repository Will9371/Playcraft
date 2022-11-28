using UnityEngine;

namespace ZMD
{
    public class Counter : MonoBehaviour
    {
        [SerializeField] IntEvent Output;
        public int count;
        
        public void Increment()
        {
            count++;
            Output.Invoke(count);
        }
        
        public void Decrement()
        {
            count--;
            Output.Invoke(count);
        }
        
        public void Increment(bool countUp)
        {
            if (countUp) Increment();
            else Decrement();
        }
        
        public void Add(int value)
        {
            count += value;
            Output.Invoke(value);
        }
        
        public void Set(int value)
        {
            count = value;
            Output.Invoke(value);
        }
    }
}
