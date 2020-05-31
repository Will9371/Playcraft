using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class RangeCheck
    {
        #pragma warning disable 0649
        [SerializeField] Vector2 closeThresholds;
        [SerializeField] Vector2 farThresholds;
        #pragma warning restore 0649
        
        public bool InRange(float value, bool priorState)
        {
            return priorState ? 
                value >= closeThresholds.x && value <= farThresholds.y:
                value >= closeThresholds.y && value <= farThresholds.x;
        }
    }
}
