using NUnit.Framework;
using Playcraft;
using UnityEngine;
using UnityEngine.Events;

public class RotateToAngle : MonoBehaviour
{
    #pragma warning disable 0649
    [SerializeField] float desiredAngle; 
    [SerializeField] float turnSpeed = 30f;
    [SerializeField] FloatEvent Angle;
    [SerializeField] UnityEvent Arrive;
    #pragma warning restore 0649
    
    float angle;
    
    bool hasArrived = true;
    bool arrived => angle == desiredAngle;
    
    public void SetRotationSpeed(float value) { turnSpeed = value; }
    
    public void SetDesiredAngle(float value) 
    {
        hasArrived = false; 
        desiredAngle = value; 
    }

    void Update()
    {    
        angle = VectorMath.RotateToAngle(angle, desiredAngle, Time.deltaTime * turnSpeed);
        Angle.Invoke(angle);
        
        if (!hasArrived && arrived) Arrive.Invoke();
        hasArrived = arrived;
    }
}
