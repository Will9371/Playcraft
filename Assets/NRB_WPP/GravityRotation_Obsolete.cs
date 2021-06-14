using System;
using System.Collections;
using UnityEngine;
using Playcraft;

// Intermediate refactor to GravityRotation, prior to breaking into components
public class GravityRotation_Obsolete : MonoBehaviour
{
    [SerializeField] float rotationTime = 1f;
    [SerializeField] Transform pivot;
    [SerializeField] Transform[] toRotate;
    [SerializeField] GravityState[] gravityStates;
    
    // TBD: communicate via UnityEvents to eliminate dependency
    [SerializeField] NonRigidbodyMovement movement;


    bool rotating = false;
    int gravityIndex = 0;

    float rotationDegrees => 360 / gravityStates.Length;
    
    void Start() 
    { 
        if (!pivot) pivot = transform; 
    }
    
    
    public void Rotate(bool clockwise)
    {
        if (rotating) return;
        StartCoroutine(RotateWorld(!clockwise));
    }

    IEnumerator RotateWorld(bool clockwise)
    {
        rotating = true;
        movement.gravity = Vector3.zero;
        
        GravityState oldState = gravityStates[gravityIndex];
        gravityIndex = RangeMath.CycleInt(gravityIndex, gravityStates.Length - 1, clockwise);
        GravityState newState = gravityStates[gravityIndex];

        Quaternion oldRotation = Quaternion.Euler(oldState.objectRotation);
        Quaternion newRotation = Quaternion.Euler(newState.objectRotation);

        float oldZAngle = pivot.localEulerAngles.z;
        float newZAngle = oldZAngle + rotationDegrees * (clockwise ? -1 : 1);
        
        float timer = 0;

        while(timer < rotationTime)
        {
            timer += Time.deltaTime;
            if (timer > rotationTime)
                timer = rotationTime;
            
            foreach (Transform rotor in toRotate)
                rotor.rotation = Quaternion.Lerp(oldRotation, newRotation, timer / rotationTime);
            
            float z = Mathf.Lerp(oldZAngle, newZAngle, timer / rotationTime);
            VectorMath.SetZRotation(pivot, z);
            
            yield return null;
        }

        movement.gravity = newState.gravityDirection;
        rotating = false;
    }

    [Serializable]
    public struct GravityState
    {
        public Vector3 gravityDirection;
        public Vector3 objectRotation;
    }
}
