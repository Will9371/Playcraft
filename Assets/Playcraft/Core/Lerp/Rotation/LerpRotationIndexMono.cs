using UnityEngine;

namespace Playcraft
{
    public class LerpRotationIndexMono : MonoBehaviour
    {
        [SerializeField] LerpRotationIndex process;
            
        void Start() { process.SetSelfIfNull(transform); }

        public void CycleDestination(bool forward) { process.CycleDestination(forward); }
        
        public void SetDestination(int newIndex) { process.SetDestination(newIndex); }
        
        /// Call continuously to rotate over time
        public void Input(float percent) { process.percent = percent; }
        
        public void SetDestinations(Vector3Array value) { SetDestinations(value.values); }
        public void SetDestinations(Vector3[] values) { process.SetDestinations(values); }
        
        public Vector3[] rotations
        {
            get => process.rotations;
            set => process.rotations = value;
        }
    }
}

