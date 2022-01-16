using System;
using UnityEngine;

namespace Playcraft
{
    /// Continuously follow a target transform at a given rotational speed
    [Serializable]
    public class SmoothFollowRotation
    {
        public Transform self;
        public Transform target;
        
        [Tooltip("Angles per second")]
        public float speed = 90f;

        Quaternion rotation => self.rotation;
        Quaternion targetRotation => target.rotation;
        Quaternion nextRotation => Quaternion.RotateTowards(rotation, targetRotation, stepDistance);
        float stepDistance => speed * Time.deltaTime;
        public float angleToTarget => Quaternion.Angle(rotation, targetRotation);

        public void Update()
        {
            if (!self || !target) return;
            self.rotation = nextRotation;
        }
    }
}
