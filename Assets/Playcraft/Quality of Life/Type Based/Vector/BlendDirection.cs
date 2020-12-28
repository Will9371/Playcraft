using System;
using System.Collections.Generic;
using UnityEngine;


public class BlendDirection : MonoBehaviour
{
    [SerializeField] List<WeightedDirection> weightedDirections = new List<WeightedDirection>();
    [SerializeField] Vector3Event Output;
    
    [SerializeField] float gizmoLineLength = 1f;
    [SerializeField] Color gizmoWeightColor = Color.green;
    [SerializeField] Color gizmoSumColor = Color.magenta;
    
    public void SetDirection0(Vector3 value) { SetDirection(value, 0); }
    public void SetDirection1(Vector3 value) { SetDirection(value, 1); }
    public void SetDirection2(Vector3 value) { SetDirection(value, 2); }

    public void SetDirection(Vector3 value, int index)
    {
        if (index >= weightedDirections.Count)
        {
            Debug.Log("Index " + index + " out of range " + weightedDirections.Count);
            return;    
        }
        
        weightedDirections[index].direction = value;
        Blend();
    }
    
    Vector3 blendedDirection;
        
    void Blend()
    {
        blendedDirection = Vector3.zero;
        
        foreach (var element in weightedDirections)
            blendedDirection += element.weightedDirection;
            
        blendedDirection = blendedDirection.normalized;
        Output.Invoke(blendedDirection);
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = gizmoWeightColor;
    
        foreach (var element in weightedDirections)
        {
            if (element.direction == Vector3.zero) continue;
            Gizmos.DrawRay(transform.position, element.weightedDirection * gizmoLineLength);
        }
        
        Gizmos.color = gizmoSumColor;
        Gizmos.DrawRay(transform.position, blendedDirection * gizmoLineLength);
    }
    
    [Serializable] public class WeightedDirection
    {
        public Vector3 direction;
        [Range(0, 1)] public float weight;
        
        public Vector3 weightedDirection => direction * weight;
    }
}
