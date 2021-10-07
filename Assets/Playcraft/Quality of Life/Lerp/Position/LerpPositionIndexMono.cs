using UnityEngine;

namespace Playcraft
{
    public class LerpPositionIndexMono : MonoBehaviour
    {
        [SerializeField] LerpPositionIndex process;

        void Start() { process.SetSelfIfNull(transform); }

        /// Move between internally-stored positions
        public void SetDestination(int newIndex) { process.SetDestination(newIndex); }
        
        /// Move towards externally defined location
        public void SetDestination(Vector3 destination) { process.SetDestination(destination); }
        
        /// Call continuously to move over time
        public void Input(float percent) { process.percent = percent; }
        
        public void SetDestinations(Vector3Array input) { SetDestinations(input.values); }
        public void SetDestinations(Vector3[] input) { process.SetDestinations(input); }
        
        public Vector3[] positions { get => process.positions; set => process.positions = value; }
        public int startIndex { get => process.startIndex; set => process.startIndex = value; }
        public int endIndex { get => process.endIndex; set => process.endIndex = value; }
        public Vector3 start { get => process.start; set => process.start = value; }
        public Vector3 end { get => process.end; set => process.end = value; }
    }
}
