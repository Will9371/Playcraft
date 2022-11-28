using System;
using UnityEngine;

namespace ZMD
{
    public class RotateAxisMono : MonoBehaviour
    {
        [SerializeField] RotateAxis process;
        void OnValidate() { process.ValidateAngle(); }
        public void SetAngle(float value) { process.SetAngle(value); }
    }
    
    [Serializable]
    public class RotateAxis
    {
        public Transform rotor;
        public Axis axis = Axis.Z;
        public bool useLocal;
        [Range(-1, 361)] public float angle;
        
        public void ValidateAngle()
        {
            if (angle < 0) angle = 360;
            else if (angle > 360) angle = 0;
            SetAngle(angle);      
        }
            
        public void SetAngle(float value)
        {
            if (!rotor) return;
            
            angle = value;
        
            // * Move to static helper
            if (useLocal)
            {
                switch (axis)
                {
                    case Axis.X: rotor.localRotation = Quaternion.Euler(angle, rotor.rotation.y, rotor.localRotation.z); break;
                    case Axis.Y: rotor.localRotation = Quaternion.Euler(rotor.rotation.x, angle, rotor.localRotation.z); break;
                    case Axis.Z: rotor.localRotation = Quaternion.Euler(rotor.rotation.x, rotor.localRotation.y, angle); break;
                }
            }
            else
            {
                switch (axis)
                {
                    case Axis.X: rotor.rotation = Quaternion.Euler(angle, rotor.rotation.y, rotor.rotation.z); break;
                    case Axis.Y: rotor.rotation = Quaternion.Euler(rotor.rotation.x, angle, rotor.rotation.z); break;
                    case Axis.Z: rotor.rotation = Quaternion.Euler(rotor.rotation.x, rotor.rotation.y, angle); break;
                }
            }
        }
    }
}
