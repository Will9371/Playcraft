using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    public class TrackCollidersMono : MonoBehaviour
    {
        [SerializeField] float refreshRate = 0.5f;
        [SerializeField] ColliderListEvent Output; 
        
        TrackColliders process = new();

        void OnEnable() { InvokeRepeating(nameof(Refresh), refreshRate, refreshRate); }
        void OnDisable() { CancelInvoke(nameof(Refresh)); }

        void OnTriggerEnter(Collider other) { process.OnTriggerEnter(other); }
        void OnTriggerExit(Collider other) { process.OnTriggerExit(other); }
        
        void Refresh() { Output.Invoke(process.Refresh()); }
    }
    
    public class TrackColliders
    {    
        public List<Collider> touching = new();

        public void OnTriggerEnter(Collider other)
        {
            if (!touching.Contains(other))
                touching.Add(other);
        }
            
        public void OnTriggerExit(Collider other)
        {
            if (touching.Contains(other))
                touching.Remove(other);
        }
            
        public List<Collider> Refresh()
        {
            touching.RemoveAll(t => !t.gameObject.activeInHierarchy);
            return touching;               
        }
    }
}
