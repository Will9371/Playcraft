using UnityEngine;

namespace Playcraft
{
    /// Access SmoothFollow as a standalone component
    /// Coordinates continuous movement and rotation to follow a target transform
    public class SmoothFollowMono : MonoBehaviour
    {
        [SerializeField] SmoothFollow process;
        
        void Update() { process.Update(); }
        public void SetTarget(Transform value) { process.SetTarget(value); }
        
        void OnValidate() 
        {
            process.SetSelf(transform); 
            process.OnValidate(); 
        }
    }
}