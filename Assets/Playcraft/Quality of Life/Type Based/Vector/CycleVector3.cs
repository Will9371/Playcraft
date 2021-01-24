using UnityEngine;

// RENAME for generality
namespace Playcraft
{
    public class CycleVector3 : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector3[] values;
        [SerializeField] Vector3Event Output;
        #pragma warning restore 0649   
        
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
