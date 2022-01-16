using System;
using UnityEngine;

namespace Playcraft
{
    /// Continuously move to follow a target transform at a given speed
    [Serializable]
    public class SmoothFollowPosition
    {
        public Transform self;
        public Transform target;
        
        [Tooltip("Distance units (default = meters) per second")]
        public float speed = 1f;

        Vector3 position => self.position;
        Vector3 targetPosition => target.position;
        Vector3 step => nextPosition - position;
        Vector3 nextPosition => Vector3.MoveTowards(position, targetPosition, stepDistance);
        float stepDistance => speed * Time.deltaTime;
        public float targetDistance => Vector3.Distance(position, targetPosition);

        public void Update()
        {
            if (!self || !target) return;
            self.Translate(step, Space.World);
        }
    }
}
