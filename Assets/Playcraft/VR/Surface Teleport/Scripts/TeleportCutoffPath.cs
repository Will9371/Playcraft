using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.VR
{
    public class TeleportCutoffPath
    { 
        readonly float maxSlope;
        readonly Transform source;
        TeleportResult result;
        SO teleportTag;
        
        public TeleportCutoffPath(Transform source, float maxSlope, SO teleportTag)
        {
            this.maxSlope = maxSlope;
            this.source = source;
            this.teleportTag = teleportTag;
        }
        
        IndexedRaycastHit indexedHit;
        CustomTags surface;
        Vector3 hitPoint;
        RaycastHit hit => indexedHit.hit;
        int cutoffIndex => indexedHit.index;
        bool validHit => surface.HasTag(teleportTag);
        float hitAngle => Vector3.Angle(hit.normal, Vector3.up);
        Trinary success => validHit && hitAngle < maxSlope ? Trinary.True : Trinary.False;

        public TeleportResult Input(ref Vector3[] path, List<IndexedRaycastHit> hitData)
        {    
            for (int i = 0; i < hitData.Count; i++)
            {
                surface = hitData[i].hit.collider.GetComponent<CustomTags>();
                if (surface) return ParseHit(ref path, hitData[i], surface);
            }
                
            return new TeleportResult(path[path.Length - 1], Trinary.Unknown);
        }
            
        private TeleportResult ParseHit(ref Vector3[] path, IndexedRaycastHit indexedHit, CustomTags surface)
        {
            this.indexedHit = indexedHit;
            this.surface = surface;

            var cutoffPath = new Vector3[cutoffIndex + 1];
                
            for (int i = 0; i < cutoffPath.Length; i++)
                cutoffPath[i] = path[i];
                
            var focus = surface.GetComponent<TeleportPoint>();
            hitPoint = focus ? focus.GetPoint() : hit.point;
                    
            var localCutoffPoint = source.InverseTransformPoint(hitPoint);
            localCutoffPoint.x = 0;
            cutoffPath[cutoffPath.Length - 1] = localCutoffPoint;
            
            path = cutoffPath;
            return new TeleportResult(hitPoint, success, surface.gameObject);
        }
    }
}
