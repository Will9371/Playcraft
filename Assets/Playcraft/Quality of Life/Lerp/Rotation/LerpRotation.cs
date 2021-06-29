using UnityEngine;

namespace Playcraft
{
    public class LerpRotation : MonoBehaviour
    {
        [SerializeField] Lerp_Rotation process;
                
        void Start() { process.SetSelfIfNull(transform); }
        
        /// Call continuously to rotate over time
        public void Input(float percent) { process.Input(percent); }
        
        public void SetPath(Vector3 start, Vector3 end) { process.SetPath(start, end); }
    }
}