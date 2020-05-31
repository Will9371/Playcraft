using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Character/Animation State")]
    public class AnimatedMoveState : ScriptableObject
    {
        public AnimationClip forward, turnLeft, turnRight;
            
        public AnimationClip GetClip(float rotation)
        {    
            if (rotation > 0.5f)
                return turnRight;
            if (rotation < -0.5f)
                return turnLeft;
            
            return forward;
        }
    }
}
