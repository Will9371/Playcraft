using UnityEngine;

namespace Playcraft
{
    public class SetRotation : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] float[] rotations;
        [SerializeField] Axis axis;
        
        Vector3 euler => self.rotation.eulerAngles;
        
        void Awake() { if (!self) self = transform; }
    
        public void Input(int index)
        {
            var newEuler = new Vector3(euler.x, euler.y, euler.z);
        
            switch (axis)
            {
                case Axis.X: newEuler.x = rotations[index]; break;
                case Axis.Y: newEuler.y = rotations[index]; break;
                case Axis.Z: newEuler.z = rotations[index]; break;
            }
        
            self.eulerAngles = newEuler;
        }
        
        public void Input(Quaternion value) { self.rotation = value; }
        public void Input(Vector3 value) { self.eulerAngles = value; }
    }   
}