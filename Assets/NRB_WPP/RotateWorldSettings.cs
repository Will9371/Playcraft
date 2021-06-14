using System;
using UnityEngine;
using Playcraft;

public class RotateWorldSettings : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] float rotationTime = 1f;
    [SerializeField] GravityState[] states;
    
    [Header("References")]
    [SerializeField] RotateWorld rotateWorld;
    [SerializeField] LerpPivotZ lerpPivotZ;
    [SerializeField] LerpRotation lerpRotation;
    [SerializeField] CycleGravity cycleGravity;
    
    void OnValidate()
    {
        rotateWorld.rotationTime = rotationTime;
        lerpPivotZ.rotationDegrees = 360 / states.Length;
    
        lerpRotation._rotations = new Vector3[states.Length];
        cycleGravity.gravityDirections = new Vector3[states.Length];
        
        for (int i = 0; i < states.Length; i++)
        {
            lerpRotation._rotations[i] = states[i].objectRotation;
            cycleGravity.gravityDirections[i] = states[i].gravityDirection;
        }
    }
    
    [Serializable]
    public struct GravityState
    {
        public Vector3 gravityDirection;
        public Vector3 objectRotation;
    }
}
