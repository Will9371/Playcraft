using UnityEngine;

namespace ZMD
{
    public class LerpPositionMono : MonoBehaviour
    {
        [SerializeField] LerpPosition process;
        
        //void Start() { process.SetSelfIfNull(transform); }
        
        /// Call continuously to move over time
        public void Input(float percent) { process.percent = percent; }
        
        /// Move towards externally defined location
        public void SetDestination(Vector3 destination) { process.SetEnd(destination); }
        
        //public void SetDirection(bool forward) { process.reverse = !forward; }
    }
}