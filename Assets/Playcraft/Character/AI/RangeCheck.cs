using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class RangeCheck
    {
        [SerializeField] Vector2 closeThresholds;
        [SerializeField] Vector2 farThresholds;
        
        public bool InRange(float value, bool priorState)
        {
            return priorState ? 
                value >= closeThresholds.x && value <= farThresholds.y:
                value >= closeThresholds.y && value <= farThresholds.x;
        }
    }
}
