using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class FollowScreenBoundedMouseInWorld
    {
        public FollowMouseOnScreen screenFollow = new FollowMouseOnScreen();
        public Camera camera;
        
        Vector3 screenSpace;
        Vector3 worldSpace;
        
        public Vector3 Update() 
        {
            screenSpace = screenFollow.Update(); 
            worldSpace = camera.ScreenToWorldPoint(screenSpace);
            return new Vector3(worldSpace.x, worldSpace.y, 0f);
        }
        
        public void OnValidate() { screenFollow.RefreshBounds(); }
    }
}
