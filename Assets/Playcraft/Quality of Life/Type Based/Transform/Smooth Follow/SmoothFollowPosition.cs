using System;
using UnityEngine;

[Serializable]
public class SmoothFollowPosition
{
    public Transform self;
    public Transform target;
    public float speed = 1f;

    Vector3 position => self.position;
    Vector3 step => nextPosition - position;
    Vector3 nextPosition => Vector3.MoveTowards(position, target.position, stepDistance);
    float stepDistance => speed * Time.deltaTime;

    public void Update()
    {
        if (!self || !target) return;
        Debug.DrawLine(position, target.position, Color.red);
        self.Translate(step);
    }
}
