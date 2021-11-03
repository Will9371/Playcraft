using UnityEngine;

namespace Playcraft
{
    public class IsColliderWithCustomTagInBounds : MonoBehaviour
    {
        [SerializeField] RespondToCustomTag respondToTag;
        [SerializeField] BoolEvent hasOne;
        TrackColliders trackColliders = new TrackColliders();
        
        public void OnTriggerEnter(Collider other)
        {
            if (respondToTag.Input(other))
                trackColliders.OnTriggerEnter(other);
                
            Output();
        }
        
        public void OnTriggerExit(Collider other)
        {
            if (respondToTag.Input(other))
                trackColliders.OnTriggerExit(other);
                
            Output();
        }
        
        void Output() { hasOne.Invoke(trackColliders.touching.Count > 0); }
    }
}
