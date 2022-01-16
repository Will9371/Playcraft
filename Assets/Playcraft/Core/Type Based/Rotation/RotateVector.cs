using UnityEngine;

namespace Playcraft
{
    public class RotateVector : MonoBehaviour
    {
        [SerializeField] Vector3 vector = Vector3.forward;
        [SerializeField] Vector3Event Output;
        
        public void RotateYAxis(float value)
        {
            vector = Quaternion.Euler(0, value * Time.deltaTime, 0) * vector;
            Output.Invoke(vector);
        }
    }
}
