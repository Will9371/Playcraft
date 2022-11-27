using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZMD
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
    
    [Serializable]
    public class GetFloatFromArray
    {
        public float[] values;
        [SerializeField] bool preventRandomRepeat;
            
        int index;
            
        public void SetValues(FloatArray values) { SetValues(values.values); }
        public void SetValues(float[] values) { this.values = values; }
            
        public float GetByIndex(int index) 
        {
            if (index < 0 || index >= values.Length)
            {
                Debug.LogError($"Cannot retrieve index {index} from array of length {values.Length}");
                return Mathf.Infinity;
            }
             
            return values[index]; 
        }
            
        public float GetRandom() { return values[GetRandomIndex()]; }
        
        public int GetRandomIndex() 
        {
            return index = preventRandomRepeat ? 
                RandomStatics.RandomIndexNotIncluding(values.Length, index) :
                Random.Range(0, values.Length);
        }
    }
}
