using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class DirectionalConstraints
    {
        [SerializeField] bool x;
        [SerializeField] bool y;
        [SerializeField] bool z;
            
        public Vector3 GetConstrainedDirection(Vector3 input)
        {
            if (x) input.x = 0f;
            if (y) input.y = 0f;
            if (z) input.z = 0f;
            return input.normalized;
        }
    }
}
