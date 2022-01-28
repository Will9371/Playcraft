﻿using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(menuName = "Playcraft/Character/Move State Machine/State")]
    public class MoveState : ScriptableObject
    {
        public AnimatedMoveState animations;
        
        public bool disableSpeedControl;
        public float moveSpeed;
    }
}
