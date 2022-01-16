using UnityEngine;

namespace Playcraft
{
    public class DetectChangeRaycastHit : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] ScreenRaycast raycast;
        [SerializeField] ColliderEvent Enter;
        [SerializeField] ColliderEvent Exit;
        #pragma warning restore 0649

        Collider hit;
        Collider priorHit;
            
        bool forceClear;
        public void ForceClear() { forceClear = true; }

        void Update()
        {
            hit = raycast.GetResult().collider;
        
            if (forceClear)
            {
                hit = null;
                forceClear = false;
            }
        
            if (hit != priorHit)
            {
                if (priorHit) Exit.Invoke(priorHit);
                if (hit) Enter.Invoke(hit);
            }
                
            priorHit = hit;
            hit = null;
        }
    }
}
