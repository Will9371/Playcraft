using UnityEngine;

namespace Playcraft
{
    public class LerpScaleMono : MonoBehaviour
    {
        [SerializeField] LerpScale process;
        
        void Start() { process.SetSelfIfNull(transform); }
        
        public void SetScale(Vector3 value) { process.SetNewScale(value); }
        
        public void SetDirection(bool forward) { process.reverse = !forward; }

        /// Call continuously to move over time
        public void Input(float percent) { process.Input(percent); }
    }
}
