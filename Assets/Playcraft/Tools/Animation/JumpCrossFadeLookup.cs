using System;
using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(menuName = "Playcraft/Character/Jump CrossFade Lookup", fileName = "Jump CrossFade Lookup")]
    public class JumpCrossFadeLookup : ScriptableObject
    {
        [SerializeField] JumpCrossFadeOverride[] overrideStates = default;
        
        public float GetTime(AnimatedMoveState state, float defaultTime)
        {
            foreach (var value in overrideStates)
                if (value.state == state)
                    return value.normalizedTime;
                    
            return defaultTime;
        }
    }
    
    [Serializable] struct JumpCrossFadeOverride
    {
        #pragma warning disable 0649
        public AnimatedMoveState state;
        [Range(0f, 1f)] public float normalizedTime;
        #pragma warning restore 0649
    }
}