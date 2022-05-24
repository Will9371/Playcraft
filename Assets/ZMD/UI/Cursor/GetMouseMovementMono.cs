using System;
using UnityEngine;

namespace ZMD
{
    public class GetMouseMovementMono : MonoBehaviour
    {
        [SerializeField] GetMouseMovement process;
        [SerializeField] Vector2Event Output;
        void Update() { Output.Invoke(process.Update()); }
    }
    
    [Serializable]
    public class GetMouseMovement
    {
        public Vector2 sensitivity = new Vector2(1f, 1f);
        
        float x;
        float y;

        /// Call this every frame to get the mouse x/y movement
        public Vector2 Update()
        {
            x = Input.GetAxis("Mouse X") * sensitivity.x;
            y = Input.GetAxis("Mouse Y") * sensitivity.y;
            return new Vector2(x, y);
        }        
    }
}
