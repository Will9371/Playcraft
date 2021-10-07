using UnityEngine;

namespace Playcraft
{
    public class LerpRotationMono : MonoBehaviour
    {
        [SerializeField] LerpRotation process;
                
        void Start() { process.SetSelfIfNull(transform); }
        
        /// Call continuously to rotate over time
        public void Input(float percent) { process.percent = percent; }
        
        public void SetPath(Vector3 start, Vector3 end) { process.SetPath(start, end); }
    }
}