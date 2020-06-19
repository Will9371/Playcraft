using UnityEngine;

namespace Playcraft
{
    // EXTEND: include x and z axis
    public class ScaleToTarget : MonoBehaviour
    {
        [SerializeField] float scaleSpeed = .5f;
        
        float targetYScale;
        
        private void Start()
        {
            targetYScale = transform.localScale.y;
        }
        
        public void InputTargetScale(Vector3 value)
        {
            InputTargetYScale(value.y);
        }

        public void InputTargetYScale(float value)
        {
            targetYScale = value;
        }
        
        private void Update()
        {
            var heightDelta = targetYScale - transform.localScale.y;
            if (Mathf.Abs(heightDelta) < .01f)
                return;
                
            var scaleDirection = heightDelta < 0 ? -1f : 1f;
            var scaleStep = scaleSpeed * scaleDirection * Time.deltaTime;
            transform.localScale += Vector3.up * scaleStep;
            
            // REFACTOR: make optional/configurable or break into separate system
            transform.position += Vector3.up * scaleStep;
        }
    }
}
