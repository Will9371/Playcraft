using UnityEngine;

namespace Playcraft
{
    public class GetFloatFromArrayMono : MonoBehaviour
    {
        [SerializeField] GetFloatFromArray process;
        [SerializeField] FloatEvent Output;
        
        public void SetValues(FloatArray values) { process.SetValues(values.values); }
        public void SetValues(float[] values) { process.SetValues(values); }
        
        public void Input(int index) 
        {
            if (index < 0 || index >= process.values.Length) return;
            Output.Invoke(process.GetByIndex(index)); 
        }
        
        public void GetRandom() { Output.Invoke(process.GetRandom()); }
    }
}
