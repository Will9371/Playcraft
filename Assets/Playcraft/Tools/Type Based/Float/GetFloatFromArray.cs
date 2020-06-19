using UnityEngine;

namespace Playcraft
{
    public class GetFloatFromArray : MonoBehaviour
    {
        [SerializeField] float[] values;
        [SerializeField] FloatEvent Output;
        
        public void SetValues(float[] values) { this.values = values; }
        
        public void Input(int index) 
        {
            if (index < 0 || index >= values.Length)
                return;
         
            Output.Invoke(values[index]); 
        }
    }
}
