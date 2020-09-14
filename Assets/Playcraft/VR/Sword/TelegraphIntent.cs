using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class TelegraphIntent : MonoBehaviour
    {
        SwingState state;
        Vector3 targetPosition;
        Vector3 priorTargetPosition;
        Vector3[] nextTargetPositions;
    
        public void Refresh(SwingState value)
        {
            state = value;
            priorTargetPosition = targetPosition;
            targetPosition = transform.position + state.direction.value.normalized * 1.25f;
            
            nextTargetPositions = new Vector3[state.nextStates.Length];
            for (int i = 0; i < nextTargetPositions.Length; i++)
                nextTargetPositions[i] = transform.position + state.nextStates[i].state.direction.value.normalized * 1.25f;
        }
        
        void OnDrawGizmos()
        {
            if (!state) return;
            
            Gizmos.color = Color.red;   
            Gizmos.DrawSphere(priorTargetPosition, .03f);         
            Gizmos.DrawSphere(targetPosition, .05f);
            Gizmos.DrawLine(priorTargetPosition, targetPosition);
            
            foreach (var target in nextTargetPositions)
            {
                Gizmos.DrawSphere(target, .03f);
                Gizmos.DrawLine(targetPosition, target);
            }
        }
    }
}
