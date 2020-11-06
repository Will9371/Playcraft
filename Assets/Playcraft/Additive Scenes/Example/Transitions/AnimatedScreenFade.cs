using System;
using Playcraft.Examples.SceneControl;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    [Serializable] public class SceneTransitionSOEvent : UnityEvent<SceneTransitionSO> { }

    public class AnimatedScreenFade : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Animator animator;
        [SerializeField] AnimationProgressEvent fadeOut;
        [SerializeField] SceneTransitionSOEvent ExitComplete;
        [SerializeField] UnityEvent EnterBegin;
        #pragma warning restore 0649
        
        AdditiveSceneController scene => AdditiveSceneController.instance;
        
        SceneTransitionSO currentTransition;
        
        void OnEnable()
        {
            animator.Play("Fade In");        
        }
        
        public void ExitScene(SceneTransitionSO transition) 
        {
            currentTransition = transition; 
            fadeOut.Play("Fade Out"); 
        }
        
        public void FadeOutComplete()
        {
            ExitComplete.Invoke(currentTransition);
            Invoke(nameof(EnterScene), currentTransition.minimumLoadScreenTime);
        }
        
        public void EnterScene()
        {
            EnterBegin.Invoke();
            scene.LoadUnload(currentTransition);        
        }
    }
}
