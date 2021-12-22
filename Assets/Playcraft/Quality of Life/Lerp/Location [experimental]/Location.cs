using System;
using UnityEngine;

namespace Playcraft
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
    }
}
