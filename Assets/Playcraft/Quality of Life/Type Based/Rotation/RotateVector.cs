using UnityEngine;

namespace Playcraft
{
    public class RotateVector : MonoBehaviour
    {
        [SerializeField] float multiplier;
        [SerializeField] Vector3Event Output;

        public Vector3 vector = Vector3.forward;
        
        public void RotateYAxis(float value)
        {
            vector = Quaternion.Euler(0, value * multiplier * Time.deltaTime, 0) * vector;
            Output.Invoke(vector);
        }
    }
}
