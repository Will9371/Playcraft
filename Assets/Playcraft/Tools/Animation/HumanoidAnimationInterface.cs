using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class HumanoidAnimationInterface : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Animator animator;    
        [SerializeField] [Range(0f, 1f)] float defaultCrossFade = .3f;
        [SerializeField] JumpCrossFadeLookup jumpFadeLookup;
        #pragma warning restore 0649

        AnimationClip priorClip;
        
        MoveState priorState;
        MoveState state;
        public void SetState(MoveState state) 
        { 
            priorState = this.state == null ? state : this.state;
            this.state = state; 
        }

        float rotation;
        public void SetRotation(Vector3 value) { rotation = value.y; }

        Vector3 moveDirection;
        public void SetMoveDirection(Vector3 value) { moveDirection = value; }
        [Serializable] public class AnimatedStateEvent : UnityEvent<AnimatedMoveState, float> { }
        [SerializeField] AnimatedStateEvent OnBeginAnimation = default;
        
                
        private void Update()
        {
            var clip = state.animations.GetClip(rotation, moveDirection);
          
            if (clip == priorClip)
                return;

            StartCoroutine(PlayRoutine(clip));
        }
        
        IEnumerator PlayRoutine(AnimationClip clip)
        {            
            var fade = jumpFadeLookup.GetTime(priorState.animations, defaultCrossFade);
            animator.CrossFade(clip.name, fade, 0);
            priorClip = clip;
            
            var length = clip.length * Mathf.Abs(animator.GetCurrentAnimatorStateInfo(0).speed);
            var fadeOutTime = length * fade;
            yield return new WaitForSeconds(fadeOutTime);
            var timeRemaining = length - (length * fade);
            
            OnBeginAnimation.Invoke(state.animations, timeRemaining);
        }
    }
}