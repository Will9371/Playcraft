using UnityEngine;

namespace ZMD
{
    public class DriftFloat : MonoBehaviour
    {
        [SerializeField] float speed = 3f;
        [SerializeField] FloatEvent Output;

        public float desiredValue;
        public void SetDesiredValue(float value) { desiredValue = value; }
        
        public float value;

        void Update()
        {
            value = VectorMath.MoveTowards(value, desiredValue, speed);
            Output.Invoke(value);
        }
    }
}
