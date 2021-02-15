using UnityEngine;

namespace Playcraft
{
    public class LerpScale : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] int defaultIndex;
        [SerializeField] Vector3[] scales;
        [SerializeField] [Range(0, 1)] float xAnchor = 0.5f;
        [SerializeField] [Range(0, 1)] float yAnchor = 0.5f;
        [SerializeField] [Range(0, 1)] float zAnchor = 0.5f;

        [Header("Debug")]
        public Vector3 start;
        public Vector3 end; 
        int index;       
            
        void Start()
        {
            if (!self) self = transform;
            
            if (scales.Length > 0f)
            {
                index = defaultIndex;
                start = scales[defaultIndex];
            }
            else start = self.localScale;
        }
        
        public void SetScaleIndex(int newIndex)
        {
            start = scales[index];
            end = scales[newIndex];
            index = newIndex;
        }
        
        public void SetScale(Vector3 scale)
        {
            start = self.localScale;
            end = scale;
            priorScale = start;
        }
        
        Vector3 priorScale;
        Vector3 scaleStep => self.localScale - priorScale;

        // Call continuously to move over time
        public void Input(float percent)
        {
            priorScale = self.localScale;
            self.localScale = Vector3.Lerp(start, end, percent);
            
            if (xAnchor != 0.5f) self.position += (.5f - xAnchor) * scaleStep.x * Vector3.right;
            if (yAnchor != 0.5f) self.position += (.5f - yAnchor) * scaleStep.y * Vector3.up;
            if (zAnchor != 0.5f) self.position += (.5f - zAnchor) * scaleStep.z * Vector3.forward;
        }
    }
}
