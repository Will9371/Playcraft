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
            
            if (positions.Length > 0)
            {
                index = defaultIndex;
                start = positions[defaultIndex];
                Input(0f);
            }
        }
        
        // Move between internally-stored positions
        public void SetDestination(int newIndex)
        {
            if (newIndex >= positions.Length)
            {
                Debug.LogError("Attempting to set destination index " + 
                                newIndex + " of " + positions.Length);
                return;
            }
        
            start = positions[index];
            end = positions[newIndex];
            index = newIndex;
        }
        
        // Move towards externally defined location
        public void SetDestination(Vector3 destination)
        {
            start = transform.position;
            end = destination;
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
