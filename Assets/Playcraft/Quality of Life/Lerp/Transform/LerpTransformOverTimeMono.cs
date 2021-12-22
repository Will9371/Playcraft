using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    // Refactor to use LerpTransformOverTime (if it works)
    public class LerpTransformOverTimeMono : MonoBehaviour
    {
        [SerializeField] LerpTransform movement;
        [SerializeField] GetPercentOverTime timer = new GetPercentOverTime();
        [SerializeField] TransformEvent reachDestination;
        
        Transform destination;
        
        public void SetDuration(float value) { timer.duration = value; }

        public void BeginMoveFromSelf(Transform newDestination)
        {
            if (!gameObject.activeInHierarchy) return;
            destination = newDestination;
            movement.SetSelfToEnd(destination);
            StartCoroutine(Move());    
        }
        
        public void BeginMoveFromSelf(Vector3 newPosition, Quaternion newRotation)
        {
            movement.SetSelfToEnd(newPosition, newRotation);
            StartCoroutine(Move());
        }
        
        IEnumerator Move() 
        { 
            yield return timer.Run(movement); 
            reachDestination.Invoke(destination);
        }
        
        void OnValidate() { movement.OnValidate(); }
    }
}