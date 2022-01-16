using UnityEngine;

namespace Playcraft
{
    // EXTEND: include x and z axis
    public class SetScale : MonoBehaviour
    {
        [SerializeField] float scaleSpeed = .5f;
        [SerializeField] [Range(0, 1)] float yAnchor;
        
        float targetYScale;
        
        void Start()
        {
            targetYScale = transform.localScale.y;
        }
        
        public void InputTargetScale(Vector3 value)
        {
            InputTargetYScale(value.y);
        }

        public void InputTargetYScale(float value) { targetYScale = value; }
        
        void Update()
        {
            var heightDelta = targetYScale - transform.localScale.y;
            if (Mathf.Abs(heightDelta) < .01f)
                return;
                
            var scaleDirection = heightDelta < 0 ? -1f : 1f;
            var scaleStep = scaleSpeed * scaleDirection * Time.deltaTime;
            transform.localScale += Vector3.up * scaleStep;
            
            transform.position += yAnchor * scaleStep * Vector3.up;
        }
    }
}
