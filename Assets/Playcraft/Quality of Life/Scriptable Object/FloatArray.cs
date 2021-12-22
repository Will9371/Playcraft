using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Data Types/Float Array", fileName = "Float Array")]
    public class FloatArray : ScriptableObject 
    { 
        public float[] values;
        public float GetRandom() { return values[Random.Range(0, values.Length)]; }
        public int length => values.Length;
    }
}