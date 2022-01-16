using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RotateToAngleMono : MonoBehaviour
    {
        [SerializeField] RotateToAngle process;

        [SerializeField] FloatEvent Angle;
        [SerializeField] UnityEvent Arrive;
        
        public void SetRotationSpeed(float value) { process.SetRotationSpeed(value); }
        public void SetDesiredAngle(float value) { process.SetDesiredAngle(value); }

        void Update()
        {
            var (angle, arrivedThisFrame) = process.Tick(Time.deltaTime);
            Angle.Invoke(angle);
            if (arrivedThisFrame) Arrive.Invoke();
        }
    }
}
