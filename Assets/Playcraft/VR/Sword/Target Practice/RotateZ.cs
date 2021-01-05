using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
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
            
            rotor.eulerAngles = new Vector3(0, 0, angle);            
        }
    }
}
