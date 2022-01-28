using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class FollowBoundedMouse
    {
        public Vector2 xBounds;
        public Vector2 yBounds;
        
        Vector3 position;

        public Vector3 Update()
        {
            position = VectorMath.MousePosition();
            position.x = Mathf.Clamp(position.x, xBounds.x, xBounds.y);
            position.y = Mathf.Clamp(position.y, yBounds.x, yBounds.y);
            return position;
        }
    }
}
