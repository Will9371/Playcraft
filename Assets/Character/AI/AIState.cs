using UnityEngine;

[CreateAssetMenu(menuName = "Character/AI/State")]
public class AIState : ScriptableObject
{
    [SerializeField] bool moveToTarget;
    [SerializeField] float stoppingDistance = 1.5f;
    [SerializeField] bool faceTarget;

    public void Tick(AI self) 
    {
        if (moveToTarget && self.targetDistance > stoppingDistance)
            self.MoveToTarget();
        
        if (faceTarget)
            self.TurnToTarget();
    }
}
