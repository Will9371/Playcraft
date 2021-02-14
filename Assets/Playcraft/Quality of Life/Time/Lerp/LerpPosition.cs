using UnityEngine;

namespace Playcraft
{
    public class LerpPosition : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] Transform self;
        [SerializeField] int defaultIndex;
        [SerializeField] bool useLocal = true;
        [SerializeField] Vector3[] positions;

        [Header("Debug")]
        public Vector3 start;
        public Vector3 end;  
        int index;      
            
        void Start()
        {
            if (!self) self = transform;
            index = defaultIndex;
            start = positions[defaultIndex];
        }
        
        public void SetDestination(int newIndex)
        {
            start = positions[index];
            end = positions[newIndex];
            index = newIndex;
        }
        
        Vector3 position;

        // Call continuously to move over time
        public void Input(float percent)
        {
            position = Vector3.Lerp(start, end, percent);
            if (useLocal) self.localPosition = position;
            else self.position = position;
        }
    }
}
