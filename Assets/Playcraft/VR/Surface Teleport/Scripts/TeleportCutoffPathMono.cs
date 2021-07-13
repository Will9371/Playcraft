using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.VR
{
    public class TeleportCutoffPathMono : MonoBehaviour
    { 
        [SerializeField] float maxSlope = 45f;
        [SerializeField] Vector3ArrayEvent OutputPath;
        [SerializeField] Vector3Event OutputEndpoint;
        [SerializeField] TrinaryEvent OutputResult;
        [SerializeField] SO teleportTag;
        
        TeleportCutoffPath cutoffPath;
        
        void Awake()
        {
            cutoffPath = new TeleportCutoffPath(transform, maxSlope, teleportTag);
        }

        public void Input(Vector3[] path, List<IndexedRaycastHit> hitData)
        {                
            var result = cutoffPath.Input(ref path, hitData);
            OutputPath.Invoke(path);    
            OutputEndpoint.Invoke(result.cutoffPoint);
            OutputResult.Invoke(result.success);
        }
    }
}
