using UnityEngine;

namespace Playcraft
{
    public class LerpRotation : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] Transform self;
        [SerializeField] int defaultIndex;
        [SerializeField] bool useLocal = true;
        [Tooltip("Enter euler angles, will be automatically converted to Quaternions")]
        public Vector3[] _rotations;
        
        [Header("Debug")]
        [SerializeField] Vector3 _start;
        [SerializeField] Vector3 _end;
        int index; 
        
        Quaternion[] rotations;
        Quaternion start;
        Quaternion end;
            
        void Start()
        {
            if (!self) self = transform;
            index = defaultIndex;
            start = rotations[defaultIndex];
        }
        
        void OnValidate() { EulerToQuaternion(); }
        
        void EulerToQuaternion()
        {
            start = Quaternion.Euler(_start);
            end = Quaternion.Euler(_end);
        
            rotations = new Quaternion[_rotations.Length];
            for (int i = 0; i < rotations.Length; i++)
                rotations[i] = Quaternion.Euler(_rotations[i]);                
        }
        
        public void SetDestination(int newIndex)
        {
            start = rotations[index];
            end = rotations[newIndex];
            index = newIndex;
        }
        
        Quaternion rotation;

        // Call continuously to move over time
        public void Input(float percent)
        {
            rotation = Quaternion.Slerp(start, end, percent);
            if (useLocal) self.localRotation = rotation;
            else self.rotation = rotation;
        }
        
        public void SetDestinations(Vector3Array input) { SetDestinations(input.values); }
        public void SetDestinations(Vector3[] input)
        {
            _rotations = new Vector3[input.Length];
            
            for (int i = 0; i < input.Length; i++)
                _rotations[i] = input[i];
                
            EulerToQuaternion();          
        }
    }
}

