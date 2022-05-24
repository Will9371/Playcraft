using UnityEngine;

namespace ZMD
{
    public class LerpScaleMono : MonoBehaviour
    {
        [SerializeField] LerpScale process;
        
        public void SetScale(Vector3 value) { process.SetNewScale(value); }
        public void FlipPath(bool forward) { process.FlipPath(); }

        /// Call continuously to move over time
        public void Input(float percent) { process.percent = percent; }
    }
}
