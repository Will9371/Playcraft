using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class FollowOnCircle
    {
        public float radius = 1f;
        public Vector3 center;
        public Vector3 direction { get; private set; }

        public Vector3 Update(Vector3 position)
        {
            direction = (position - center).normalized;
            return center + direction * radius;
        }
    }
}
