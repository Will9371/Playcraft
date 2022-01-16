using UnityEngine;

namespace Playcraft
{
    public class LerpColorIndexMono : MonoBehaviour
    {
        [SerializeField] LerpColorIndex process;
        
        void Start() { process.Initialize(); }
            
        /// Move between internally-stored positions
        public void SetDestination(int newIndex) { process.SetDestination(newIndex); }
            
        /// Move towards externally defined location
        public void SetTargetColor(Color value) { process.SetTargetColor(value); }
            
        /// Call continuously to move over time
        public void Input(float percent) { process.Input(percent); }
    }
}
