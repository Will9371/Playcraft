using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class TrackColliders
    {    
        public List<Collider> touching = new List<Collider>();

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
