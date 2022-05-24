using System;
using UnityEngine;

namespace ZMD
{
    /// Data elements of Transform
    [Serializable]
    public struct Location
    {
        public Vector3 position;
        public Quaternion rotation;
        public Vector3 scale;

        public Location(Transform transform)
        {
            position = transform.position;
            rotation = transform.rotation;
            scale = transform.localScale;
        }
        
        public Location(Vector3 position, Quaternion rotation, Vector3 scale)
        {
            this.position = position;
            this.rotation = rotation;
            this.scale = scale;
        }
        
        public Location(Vector3 position, Quaternion rotation)
        {
            this.position = position;
            this.rotation = rotation;
            scale = Vector3.one;
        }
    }
}
