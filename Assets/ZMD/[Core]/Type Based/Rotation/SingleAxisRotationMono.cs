using System;
using UnityEngine;

namespace ZMD
{
    public class SingleAxisRotationMono : MonoBehaviour
    {
        [SerializeField] SingleAxisRotation process;
        public void Rotate(float input) { process.Rotate(input); }
    }
    
    [Serializable]
    public class SingleAxisRotation
    {
        [Header("References")]
        public Transform self;
        
        [Header("Settings")]
        public Axis rotationAxis;
        public bool invert;
        public bool clamp;
        public Vector2 range;
        
        float value;

        public void Rotate(float input)
        {
            value += input * (invert ? -1f : 1f);
            if (clamp) value = RangeMath.ApplyMinMax(value, range);
            
            switch (rotationAxis)
            {
                case Axis.X: self.eulerAngles = new Vector3(value, self.eulerAngles.y, self.eulerAngles.z); break;
                case Axis.Y: self.eulerAngles = new Vector3(self.eulerAngles.x, value, self.eulerAngles.z); break;
                case Axis.Z: self.eulerAngles = new Vector3(self.eulerAngles.x, self.eulerAngles.y, value); break;
            }
        }        
    }
}
