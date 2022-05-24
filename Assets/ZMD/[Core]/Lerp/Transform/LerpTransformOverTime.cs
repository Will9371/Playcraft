using System;
using System.Collections;
using UnityEngine;

namespace ZMD
{
    [Serializable]
    public class LerpTransformOverTime
    {
        [SerializeField] LerpTransform movement;
        [SerializeField] GetPercentOverTime timer;
        
        public float moveTime => timer.duration;
        
        public void SetDuration(float value) { timer.duration = value; }

        /// Begin movement from current location to destination
        public IEnumerator Move(Transform destination) { yield return Move(destination.position, destination.rotation); }

        /// Begin movement from current location to destination
        public IEnumerator Move(Vector3 newPosition, Quaternion newRotation)
        {
            movement.SetSelfToEnd(newPosition, newRotation);
            yield return timer.Run(movement); 
        }

        public void OnValidate() { movement.OnValidate(); }
        
        public void Interrupt() { timer.interruptFlag = true; }
    }
}
