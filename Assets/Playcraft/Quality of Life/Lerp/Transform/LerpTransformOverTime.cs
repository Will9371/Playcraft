using System;
using System.Collections;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class LerpTransformOverTime
    {
        public GameObject self;
        [SerializeField] LerpTransform movement;
        [SerializeField] GetPercentOverTime timer;
        
        public float moveTime => timer.duration;
        
        public void SetDuration(float value) { timer.duration = value; }

        public IEnumerator BeginMoveFromSelf(Transform destination) { yield return BeginMoveFromSelf(destination.position, destination.rotation); }

        // RENAME: Move (mention self in comment)
        public IEnumerator BeginMoveFromSelf(Vector3 newPosition, Quaternion newRotation)
        {
            movement.SetSelfToEnd(newPosition, newRotation);
            yield return timer.Run(movement); 
        }

        public void OnValidate() 
        {
            if (self) movement.self = self.transform;
            movement.OnValidate(); 
        }
        
        public void SetUseLocal(bool value) { movement.useLocal = value; }
    }
}
