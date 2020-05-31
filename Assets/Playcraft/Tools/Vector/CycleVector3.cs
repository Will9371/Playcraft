using UnityEngine;

namespace Playcraft
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
        
        public void InputIndex(int index)
        {
            this.index = index;
            Output.Invoke(values[index]);
        }
    }
}
