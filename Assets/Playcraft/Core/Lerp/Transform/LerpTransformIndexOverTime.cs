using System;
using System.Collections;
using UnityEngine;


namespace Playcraft
{
    /// Lerp-based movement & rotation from current location to indexed location
    [Serializable]
    public class LerpTransformIndexOverTime
    {
        [Tooltip("Leave blank to set locations manually")]
        public Transform destinationParent;
        [SerializeField] LerpTransformIndex movement;
        public GetPercentOverTime timer;
            
        public float duration => timer.duration;
        
        public void OnValidate() 
        { 
            if (destinationParent) 
                SetDestinations(StaticHelpers.GetChildren(destinationParent)); 
                    
            movement?.OnValidate(); 
        }
        
        public void SetSelf(Transform value) { movement.self = value; }
        public void SetUseLocal(bool value) { movement.SetUseLocal(value); }

        public void SetDuration(float value) { timer.duration = value; }

        public IEnumerator Move(int toIndex) 
        {
            movement.SetSelfToEnd(toIndex);
            yield return timer.Run(movement);
        }
            
        public IEnumerator MoveToRandom() { yield return Move(movement.GetRandomDestination()); }
        
        public void SetDestinations(Transform[] values) { movement.SetDestinations(values); }
        
        public void SetCurve(bool useCurve, AnimationCurve curve) { movement.SetCurve(useCurve, curve); }
    }
}
