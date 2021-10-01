using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class RotateAxis
    {
        [SerializeField] Transform rotor;
        [SerializeField] Axis axis = Axis.Z;
        [SerializeField] [Range(-1, 361)] float editorAngle;
        
        public void ValidateAngle()
        {
            if (editorAngle < 0) editorAngle = 360;
            else if (editorAngle > 360) editorAngle = 0;
            SetAngle(editorAngle);      
        }
            
        public void SetAngle(float value)
        {
            switch (axis)
            {
                case Axis.X: rotor.eulerAngles = new Vector3(value, rotor.eulerAngles.y, rotor.eulerAngles.z); break;
                case Axis.Y: rotor.eulerAngles = new Vector3(rotor.eulerAngles.x, value, rotor.eulerAngles.z); break;
                case Axis.Z: rotor.eulerAngles = new Vector3(rotor.eulerAngles.x, rotor.eulerAngles.y, value); break;
            }           
        }
    }
}
