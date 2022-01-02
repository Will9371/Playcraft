using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class LerpLocationOverTime : MonoBehaviour
    {
        [SerializeField] LerpLocation movement;
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
        
        IEnumerator Move() 
        { 
            yield return timer.Run(movement); 
            reachDestination.Invoke(destination);
        }
        
        void OnValidate() { movement.OnValidate(); }
    }
}