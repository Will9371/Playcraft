﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class HumanoidAnimationInterface : MonoBehaviour
    {
        Animator animator;    
        [SerializeField] [Range(0f, 1f)] float defaultCrossFade = .3f;

        new string animation;
        string priorAnimation;
        
        MoveState state;
        float rotation;
        
        AnimatorStateInfo animatorInfo { get { return animator.GetCurrentAnimatorStateInfo(0); } }
        [Serializable] public class AnimatedStateEvent : UnityEvent<AnimatedMoveState, float> { }
        #pragma warning disable 0649
        [SerializeField] AnimatedStateEvent OnBeginAnimation;
        #pragma warning restore 0649
        
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
        
        public void SetState(MoveState state)
        {
            this.state = state;
        }
        
        public void SetRotation(Vector3 rotation)
        {
            this.rotation = rotation.y;
        }
        
        private void Update()
        {
            animation = state.animations.GetClip(rotation).name;
          
            if (animation == priorAnimation)
                return;

            StartCoroutine(PlayRoutine());
        }
        
        IEnumerator PlayRoutine()
        {
            animator.CrossFade(animation, defaultCrossFade, 0);
            priorAnimation = animation;
            
            var fadeOutTime = animatorInfo.length * defaultCrossFade;
            yield return new WaitForSeconds(fadeOutTime);
            var timeRemaining = animatorInfo.length - (animatorInfo.length * defaultCrossFade);
            
            OnBeginAnimation.Invoke(state.animations, timeRemaining);
        }
    }
}