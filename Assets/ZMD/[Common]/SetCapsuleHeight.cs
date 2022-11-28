using System;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class SetCapsuleHeight
    {
        [SerializeField] CapsuleCollider capsule;
        [SerializeField] float changeSpeed = 1f;
        
        [ReadOnly] public float targetHeight;
        public void Start() { targetHeight = capsule.height; }
        
        float heightDelta;
        public void Update()
        {
            heightDelta = targetHeight - capsule.height;
            if (Mathf.Abs(heightDelta) < .01f)
                return;
                
            var scaleDirection = heightDelta < 0 ? -1f : 1f;
            var scaleStep = changeSpeed * targetHeight * scaleDirection * Time.deltaTime;
            capsule.height += scaleStep;
        }
    }
}