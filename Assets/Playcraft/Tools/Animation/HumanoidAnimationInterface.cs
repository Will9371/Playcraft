using System;
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
        public void SetState(MoveState state) { this.state = state; }

        float rotation;
        public void SetRotation(Vector3 value) { rotation = value.y; }

        Vector3 moveDirection;
        public void SetMoveDirection(Vector3 value) { moveDirection = value; }
        
        AnimatorStateInfo animatorInfo { get { return animator.GetCurrentAnimatorStateInfo(0); } }
        [Serializable] public class AnimatedStateEvent : UnityEvent<AnimatedMoveState, float> { }
        #pragma warning disable 0649
        [SerializeField] AnimatedStateEvent OnBeginAnimation;
        #pragma warning restore 0649
        
        
        private void Awake()
        {
            animator = GetComponent<Animator>();
        }
                
        private void Update()
        {
            animation = state.animations.GetClip(rotation, moveDirection).name;
          
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