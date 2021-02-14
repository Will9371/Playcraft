using UnityEngine;

namespace Playcraft
{
    public class RotateZ : MonoBehaviour
    {
        [SerializeField] Transform rotor;
        [SerializeField] [Range(-1, 361)] float angle;
        
        void OnValidate()
        {
            SetAngle(angle);
        }
        
        public void SetAngle(float value)
        {
            angle = value;
            
            if (angle < 0) angle = 360;
            else if (angle > 360) angle = 0;
            
            rotor.eulerAngles = new Vector3(rotor.eulerAngles.x, rotor.eulerAngles.y, angle);            
        }
    }
}
