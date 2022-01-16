using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RotateAxisToAngleFromArray : MonoBehaviour
    {
        [SerializeField] GetFloatFromArray angles;
        [SerializeField] RotateToAngle rotation;
        [SerializeField] RotateAxis axis;
        [SerializeField] UnityEvent OnArrive;
        
        public void SetAngles(FloatArray values) { angles.SetValues(values.values); }
        public void SetAngles(float[] values) { angles.SetValues(values); }
        
        public void SetDestinationByIndex(int index) { SetDestination(angles.GetByIndex(index)); }
        public void SetRandomDestination() { SetDestination(angles.GetRandom()); }
        void SetDestination(float value) { rotation.SetDesiredAngle(value); }
        
        public void SetRotationSpeed(float value) { rotation.SetRotationSpeed(value); }

        void Update()
        {
            var (angle, arrivedThisFrame) = rotation.Tick(Time.deltaTime);
            axis.SetAngle(angle);
            if (arrivedThisFrame) OnArrive.Invoke();
        }
        
        void OnValidate() { axis.ValidateAngle(); }
    }
}
