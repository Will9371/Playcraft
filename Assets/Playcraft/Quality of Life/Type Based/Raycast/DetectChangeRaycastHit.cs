using Playcraft;
using UnityEngine;


public class DetectChangeRaycastHit : MonoBehaviour
{
    [SerializeField] ScreenRaycast raycast;
    [SerializeField] ColliderEvent Enter;
    [SerializeField] ColliderEvent Exit;

    Collider hit;
    Collider priorHit;
    
    //public void SetHit(RaycastHit value) { hit = value.collider; }
    
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
