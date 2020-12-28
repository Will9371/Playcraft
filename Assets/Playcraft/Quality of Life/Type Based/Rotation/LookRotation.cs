using UnityEngine;

namespace Playcraft
{
    public class LookRotation : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] Vector3 axis;
        
        void Start()
        {
            if (!self) self = transform;
            if (axis == Vector3.zero) axis = Vector3.up;
        }

        public void Input(Vector3 value)
        {
            if (value == Vector3.zero) return;
            var rotation = Quaternion.LookRotation(value, axis);
            self.rotation = rotation;
        }
    }
}
