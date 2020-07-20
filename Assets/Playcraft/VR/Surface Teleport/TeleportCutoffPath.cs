using System;
using System.Collections.Generic;
using Playcraft;
using UnityEngine;
using UnityEngine.Events;

// REPLACE WITH TRINARY
//public enum TeleportResult { Success, Blocked, Empty }

public class TeleportCutoffPath : MonoBehaviour
{    
    [SerializeField] float maxSlope = 45f;
    
    [SerializeField] Vector3ArrayEvent OutputPath;
    [SerializeField] Vector3Event OutputEndpoint;
    [SerializeField] TrinaryEvent OutputResult;


    public void Input(Vector3[] path, List<IndexedRaycastHit> hitData)
    {
        IndexedRaycastHit indexedHit = new IndexedRaycastHit();
    
        for (int i = 0; i < hitData.Count; i++)
        {
            var surface = hitData[i].hit.collider.GetComponent<TeleportSurface>();
        
            if (surface)
            {
                ParseHit(path, hitData[i], surface);
                return;
            }
        }
        
        Output(path, path[path.Length - 1], Trinary.Unknown);
    }
    
    private void ParseHit(Vector3[] path, IndexedRaycastHit indexedHit, TeleportSurface surface)
    {
        var hit = indexedHit.hit;
        var cutoffIndex = indexedHit.index;
        var cutoffPoint = hit.point;
        var validHit = surface.valid;
        var cutoffPath = new Vector3[cutoffIndex + 1];
        var hitAngle = Vector3.Angle(hit.normal, Vector3.up);
        var result = validHit && hitAngle < maxSlope ? Trinary.True : Trinary.False;
        
        for (int i = 0; i < cutoffPath.Length; i++)
            cutoffPath[i] = path[i];
            
        var localCutoffPoint = transform.InverseTransformPoint(cutoffPoint);
        localCutoffPoint.x = 0;
        cutoffPath[cutoffPath.Length - 1] = localCutoffPoint;
                            
        Output(cutoffPath, cutoffPoint, result);
    }
    
    private void Output(Vector3[] path, Vector3 endpoint, Trinary result)
    {
        OutputPath.Invoke(path);    
        OutputEndpoint.Invoke(endpoint);
        OutputResult.Invoke(result);
    }
}
