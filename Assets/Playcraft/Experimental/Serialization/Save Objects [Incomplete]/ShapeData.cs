// Credit: Game Dev Guide
// Source: https://www.youtube.com/watch?v=5roZtuqZyuw

// INCOMPLETE

using System;
using UnityEngine;

namespace ZMD.Saving
{
    [Serializable]
    public enum ShapeType
    {
        Cube,
        Sphere,
    }

    [Serializable]
    public class ShapeData
    {
        public string id;
        public ShapeType shapeType;
        public Vector3 position;
        public Quaternion rotation;
    }
}

