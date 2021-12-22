using System;
using UnityEngine;

[Serializable]
public class SmoothFollow
{
    public Transform target;
    public SmoothFollowPosition position;
    public SmoothFollowRotation rotation;
    
    public void Update()
    {
        position.Update();
        rotation.Update();
    }
    
    public void OnValidate()
    {
        SetTarget(target);
    }
    
    public void SetTarget(Transform value)
    {
        target = value;
        position.target = value;
        rotation.target = value;
    }
}
