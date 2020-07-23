using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.Optimized.VR
{
    public class TeleportCutoffPath
    { 
        readonly float maxSlope;
        readonly Transform source;
        TeleportResult result;
        
        public TeleportCutoffPath(Transform source, float maxSlope)
        {
            this.maxSlope = maxSlope;
            this.source = source;
        }

        public TeleportResult Input(ref Vector3[] path, List<IndexedRaycastHit> hitData)
        {    
            for (int i = 0; i < hitData.Count; i++)
            {
                surface = hitData[i].hit.collider.GetComponent<ComponentTags>();
                if (!surface) continue;
                return ParseHit(ref path, hitData[i], surface);
            }
                
            return new TeleportResult(path[path.Length - 1], Trinary.Unknown);
        }
        
        IndexedRaycastHit indexedHit;
        ComponentTags surface;
        RaycastHit hit => indexedHit.hit;
        int cutoffIndex => indexedHit.index;
        Vector3 cutoffPoint => hit.point;
        bool validHit => surface.IsValid(TagID.Teleport);
        float hitAngle => Vector3.Angle(hit.normal, Vector3.up);
        Trinary success => validHit && hitAngle < maxSlope ? Trinary.True : Trinary.False;
            
        private TeleportResult ParseHit(ref Vector3[] path, IndexedRaycastHit indexedHit, ComponentTags surface)
        {
            this.indexedHit = indexedHit;
            this.surface = surface;

            var cutoffPath = new Vector3[cutoffIndex + 1];
                
            for (int i = 0; i < cutoffPath.Length; i++)
                cutoffPath[i] = path[i];
                    
            var localCutoffPoint = source.InverseTransformPoint(cutoffPoint);
            localCutoffPoint.x = 0;
            cutoffPath[cutoffPath.Length - 1] = localCutoffPoint;
            
            path = cutoffPath;
            return new TeleportResult(cutoffPoint, success);
        }
    }
}
