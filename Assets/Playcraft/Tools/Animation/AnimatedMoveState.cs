using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Character/Animation State")]
    public class AnimatedMoveState : ScriptableObject
    {
        public AnimationClip idle, idleLeft, idleRight, forward, 
        turnLeft, turnRight, strafeLeft, strafeRight, backward;
            
        public AnimationClip GetClip(float rotation, Vector3 moveDirection)
        {
            if (moveDirection.magnitude < 0.2f)
            {
                if (Mathf.Abs(rotation) > 0.5f)   
                    return rotation > 0 ? idleRight : idleLeft;
            
                return idle;
            }
        
            if (Mathf.Abs(rotation) > 0.5f)   
                return rotation > 0 ? turnRight : turnLeft;
            if (Mathf.Abs(moveDirection.x) >= Mathf.Abs(moveDirection.z))
                return moveDirection.x >= 0 ? strafeRight : strafeLeft; 
                        
            return moveDirection.z >= 0 ? forward : backward;
        }
    }
}
