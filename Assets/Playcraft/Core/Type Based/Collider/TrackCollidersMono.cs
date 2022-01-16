using UnityEngine;

namespace Playcraft
{
    public class TrackCollidersMono : MonoBehaviour
    {
        [SerializeField] float refreshRate = 0.5f;
        [SerializeField] ColliderListEvent Output; 
        
        TrackColliders process = new TrackColliders();

        void OnEnable() { InvokeRepeating(nameof(Refresh), refreshRate, refreshRate); }
        void OnDisable() { CancelInvoke(nameof(Refresh)); }

        void OnTriggerEnter(Collider other) { process.OnTriggerEnter(other); }
        void OnTriggerExit(Collider other) { process.OnTriggerExit(other); }
        
        void Refresh() { Output.Invoke(process.Refresh()); }
    }
}
