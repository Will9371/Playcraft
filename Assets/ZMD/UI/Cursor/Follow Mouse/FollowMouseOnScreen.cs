using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class FollowMouseOnScreen
    {
        [NonSerialized] public FollowBoundedMouse process = new FollowBoundedMouse();
        
        public float buffer;
        
        public void RefreshBounds()
        {
            process.xBounds = new Vector2(buffer, Screen.width - buffer);
            process.yBounds = new Vector2(buffer, Screen.height - buffer);
        }
        
        public Vector3 Update() { return process.Update(); }
    }
}
