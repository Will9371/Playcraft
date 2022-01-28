using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "ZMD/Data Types/Float Array", fileName = "Float Array")]
    public class FloatArray : ScriptableObject 
    { 
        public float[] values;
        public float GetRandom() { return values[Random.Range(0, values.Length)]; }
        public int length => values.Length;
    }
}