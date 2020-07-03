using UnityEngine;

namespace Playcraft
{
    public class DriftFloat : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float speed = 3f;
        [SerializeField] FloatEvent Output;
        #pragma warning restore 0649

        public float value;
        public float desiredValue;
        public void SetDesiredValue(float value) { desiredValue = value; }

        private void Update()
        {
            value = VectorMath.MoveTowards(value, desiredValue, speed);
            Output.Invoke(value);
        }
    }
}
