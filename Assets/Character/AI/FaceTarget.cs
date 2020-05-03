﻿using UnityEngine;

public class FaceTarget : MonoBehaviour
{
    Transform target;
    public void SetTarget(Transform target) { this.target = target; }
    public void ClearTarget() { target = null; }

    [SerializeField] float minStartTurnAngle = 45f;
    [SerializeField] float minStopTurnAngle = 2f;
    
    Vector3 forward { get { return transform.forward; } }
    Vector3 targetVector { get { return target == null ? Vector3.zero : target.position - transform.position; } }
    float angleToTarget { get { return StaticAxis.AngleAroundAxis(forward, targetVector, Vector3.up); } }
    float targetAngleAbs { get { return Mathf.Abs(angleToTarget); } }
    
    bool isTurning;
    
    [SerializeField] TurnAxisEvent BroadcastTurnAxis;

    void Update()
    {
        if (!target) return;
            
        isTurning = !isTurning && targetAngleAbs >= minStartTurnAngle || 
                     isTurning && targetAngleAbs >= minStopTurnAngle;  
                     
        if (isTurning)
            BroadcastTurnAxis.Invoke(Axis.Y, angleToTarget > 0f);
    }
}
