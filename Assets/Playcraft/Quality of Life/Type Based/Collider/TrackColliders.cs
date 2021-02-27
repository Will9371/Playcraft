using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class TrackColliders : MonoBehaviour
    {
        [SerializeField] float refreshRate = 0.5f;
        [SerializeField] ColliderListEvent Output; 
        
        Track_Colliders process = new Track_Colliders();

        void OnEnable() { InvokeRepeating(nameof(Refresh), refreshRate, refreshRate); }
        void OnDisable() { CancelInvoke(nameof(Refresh)); }

        void OnTriggerEnter(Collider other) { process.OnTriggerEnter(other); }
        void OnTriggerExit(Collider other) { process.OnTriggerExit(other); }
        
        void Refresh() { Output.Invoke(process.Refresh()); }
    }
    
    public class Track_Colliders
    {    
        readonly List<Collider> touching = new List<Collider>();

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
