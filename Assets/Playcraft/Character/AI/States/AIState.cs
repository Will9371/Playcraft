using System;
using UnityEngine;

namespace Playcraft.AI
{
    [CreateAssetMenu(menuName = "Playcraft/Character/AI/State")]
    public class AIState : SO
    {
        public AIExitCondition[] exitConditions;
        
        public void CheckExitConditions(AIStateMachine self)
        {
            foreach (var condition in exitConditions)
            {
                if (condition.IsSatisfied(self))
                {
                    self.SetState(condition.nextState);
                    break;
                }
            }
        }
    }

    // * Refactor: use ScriptableObjects instead of enum + switch
    [Serializable] 
    public class AIExitCondition
    {
        public AIExitConditionID id;
        public AIState nextState;
        
        [Header("Conditional")]
        public float timeLimit;
        public float distance;
        
        public bool IsSatisfied(AIStateMachine self)
        {
            switch (id)
            {
                case AIExitConditionID.SeeTarget: return self.canSeeTarget;
                case AIExitConditionID.CantSeeTarget: return !self.canSeeTarget;
                case AIExitConditionID.TimeLimit: return Time.time - self.enterStateTime >= timeLimit;
                case AIExitConditionID.CloseToTarget: return self.targetDistance <= distance;
                case AIExitConditionID.NotCloseToTarget: return self.targetDistance > distance;
            }
            
            return false;
        }
    }

    public enum AIExitConditionID
    {
        SeeTarget,
        CantSeeTarget,
        TimeLimit,
        CloseToTarget,
        NotCloseToTarget,
    }
}
