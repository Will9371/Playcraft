using UnityEngine;

namespace Playcraft
{
    public class GetFloatFromArray : MonoBehaviour
    {
        [SerializeField] float[] values;
        [SerializeField] FloatEvent Output;
        [SerializeField] bool preventRandomRepeat;
        
        int index;
        
        public void SetValues(FloatArray values) { SetValues(values.values); }
        public void SetValues(float[] values) { this.values = values; }
        
        public void Input(int index) 
        {
            if (index < 0 || index >= values.Length)
                return;
         
            Output.Invoke(values[index]); 
        }
        
        public void GetRandom()
        {
            index = RandomStatics.RandomIndexNotIncluding(values.Length, index);
            //Random.Range(0, values.Length);
            Output.Invoke(values[index]);
        }
    }
}
