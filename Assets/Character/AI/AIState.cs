using System;
using UnityEngine;

[CreateAssetMenu(menuName = "Character/AI/State")]
public class AIState : ScriptableObject
{
    [SerializeField] bool moveToTarget;
    [SerializeField] bool faceTarget;
    [SerializeField] RangeCheck followRange;

    public void Tick(AI self) 
    {
        var inRange = followRange.InRange(self.targetDistance, self.inRangeOfTarget);
        self.inRangeOfTarget = inRange;
        
        if (moveToTarget && inRange)
            self.MoveToTarget();           
        if (faceTarget)
            self.TurnToTarget();
    }
}

[Serializable]
public class RangeCheck
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
