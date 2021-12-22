using System;
using UnityEngine;

[Serializable]
public class SmoothFollowRotation
{
    public Transform self;
    public Transform target;
    public float speed = 90f;

    Quaternion rotation => self.rotation;
    Quaternion nextRotation => Quaternion.RotateTowards(rotation, target.rotation, stepDistance);
    float stepDistance => speed * Time.deltaTime;

    public void Update()
    {
        if (!self || !target) return;
        self.rotation = nextRotation;
    }
}
