using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class TrackColliders : MonoBehaviour
    {
        enum RefreshMethod { Timer, Change }
    
        [Tooltip("If set to Timer, will filter out inactive colliders and output result " +
                 "on a timed interval set by refreshRate.  If set to Change, refresh will" +
                 "occur whenever a collider enters or exits.")]
        [SerializeField] RefreshMethod refreshMethod;
        [SerializeField] float refreshRate = 0.5f;
        [SerializeField] ColliderListEvent Output; 
        List<Collider> touching = new List<Collider>();
        
        bool refreshOnTimer => refreshMethod == RefreshMethod.Timer;
        bool refreshOnChange => refreshMethod == RefreshMethod.Change;

        void OnEnable() 
        {
            if (refreshOnTimer) 
                InvokeRepeating(nameof(Refresh), refreshRate, refreshRate); 
        }
        
        void OnDisable() 
        {
            if (refreshOnTimer) 
                CancelInvoke(nameof(Refresh)); 
        }

        void OnTriggerEnter(Collider other)
        {
            if (!touching.Contains(other))
                touching.Add(other);
                
            if (refreshOnChange)
                Refresh();
        }
        
        void OnTriggerExit(Collider other)
        {
            if (touching.Contains(other))
                touching.Remove(other);
                
            if (refreshOnChange)
                Refresh();
        }
        
        void Refresh()
        {
            foreach (var touch in touching)
                if (!touch.gameObject.activeInHierarchy)
                    touching.Remove(touch);
                    
            Output.Invoke(touching);
        }
    }
}
