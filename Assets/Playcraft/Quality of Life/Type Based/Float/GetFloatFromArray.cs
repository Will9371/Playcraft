using UnityEngine;

namespace Playcraft
{
    public class GetFloatFromArray : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float[] values;
        [SerializeField] FloatEvent Output;
        #pragma warning restore 0649
        
        public void SetValues(float[] values) { this.values = values; }
        
        public void Input(int index) 
        {
            if (index < 0 || index >= values.Length)
                return;
         
            Output.Invoke(values[index]); 
        }
        
        public void GetRandom()
        {
            var index = Random.Range(0, values.Length);
            Output.Invoke(values[index]);
        }
    }
}
