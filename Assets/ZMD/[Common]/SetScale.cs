using System;
using UnityEngine;

namespace ZMD
{
    // EXTEND: include x and z axis
    [Serializable]
    public class SetScale
    {
        MonoBehaviour mono;
        Transform transform => mono.transform;
        
        public float scaleSpeed = .5f;
        [Range(0, 1)] public float yAnchor;
        
        float targetYScale;
        
        public void Start(MonoBehaviour mono)
        {
            this.mono = mono;
            targetYScale = transform.localScale.y;
        }
        
        public void InputTargetScale(Vector3 value) { InputTargetYScale(value.y); }

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
