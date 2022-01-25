using UnityEngine;

// RENAME for generality
namespace ZMD
{
    public class CycleVector3 : MonoBehaviour
    {
        [SerializeField] Vector3[] values;
        [SerializeField] Vector3Event Output;
        
        int index;
        
        public void Cycle()
        {
            index++;
            
            if (index >= values.Length)
                index = 0;
                
            InputIndex(index);
        }
        
        public void Randomize()
        {
            var index = Random.Range(0, values.Length);
            InputIndex(index);
        }
        
        public void InputIndex(int index)
        {
            this.index = index;
            Output.Invoke(values[index]);
        }
    }
}
