using UnityEngine;

namespace Playcraft.VR
{
    public class RespondToCut : MonoBehaviour
    {
        [SerializeField] SO[] validTags;
        [SerializeField] float minimumSpeed;
        [SerializeField] FloatEvent Success;
        
        void OnTriggerEnter(Collider other)
        {
            // Debug.Log("Collision detected with " + other.collider.name);
            if (!StaticHelpers.ColliderHasCustomTag(other, validTags)) return;
            
            float hitSpeed = 0f;    // other object tracks speed, access here
            
            if (hitSpeed >= minimumSpeed)
                Success.Invoke(hitSpeed);
        }
    }
}
