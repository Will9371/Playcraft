using UnityEngine;

namespace Playcraft
{
    public class LerpPositionMono : MonoBehaviour
    {
        [SerializeField] LerpPosition process;
        
        void Start() { process.SetSelfIfNull(transform); }
        
        /// Call continuously to move over time
        public void Input(float percent) { process.percent = percent; }
        
        /// Move towards externally defined location
        public void SetDestination(Vector3 destination) { process.SetEnd(destination); }
        
        public void SetDirection(bool forward) { process.reverse = !forward; }
        
        #region Process Variable Access
        
        public Vector3 start
        {
            get => process.start;
            set => process.start = value;
        }
            
        public Vector3 end
        {
            get => process.end;
            set => process.end = value;
        }
        
        #endregion
    }
}