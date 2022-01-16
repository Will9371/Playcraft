using UnityEngine;

namespace Playcraft
{
    public class LerpTransformIndexMono : MonoBehaviour
    {
        [SerializeField] LerpTransformIndex process;
        
        /// Move between internally-stored positions
        public void SetDestination(int newIndex) { process.SetDestination(newIndex); }
        
        public void SetSelfToEnd(int newIndex) { process.SetSelfToEnd(newIndex); }
        
        /// Move towards externally defined location
        public void SetDestination(Transform destination) { process.SetDestination(destination); }
        
        /// Call continuously to move over time
        public void Input(float percent) { process.percent = percent; }
        
        public void SetDestinations(Transform[] input) { process.SetDestinations(input); }
        
        public Transform[] locations { get => process.locations; set => process.locations = value; }
        public int startIndex { get => process.startIndex; set => process.startIndex = value; }
        public int endIndex { get => process.endIndex; set => process.endIndex = value; }
        public Transform start { get => process.start; set => process.start = value; }
        public Transform end { get => process.end; set => process.end = value; }
        
        public void OnValidate() { process.OnValidate(); }
    }
}
