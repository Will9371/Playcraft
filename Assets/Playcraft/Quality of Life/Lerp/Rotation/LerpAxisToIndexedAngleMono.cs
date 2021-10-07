using UnityEngine;

namespace Playcraft
{
    public class LerpAxisToIndexedAngleMono : MonoBehaviour
    {
        [SerializeField] LerpAxisToIndexedAngle process;

        void OnValidate() { process.axis.ValidateAngle(); }

        public void Input(float percent) { process.Input(percent); }
        public void SetNewDestination(int newIndex) { process.SetNewDestination(newIndex); }
        public void SetRandomDestination() { process.SetRandomDestination(); }
        public void SetValues(FloatArray values) { process.SetValues(values); }
    }
}
