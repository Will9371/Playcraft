using System.Collections;
using UnityEngine;

// Extension to ProgressEvent: sends animation as source of time interval
namespace Playcraft
{
    public class AnimationProgressEvent : MonoBehaviour 
    {
        #pragma warning disable 0649
        [SerializeField] ProgressEvent sequence;
        [SerializeField] Animator animator;
        #pragma warning restore 0649
        
        float percent => stateInfo.normalizedTime; 
        AnimatorStateInfo stateInfo => animator.GetCurrentAnimatorStateInfo(0); 
        
        void Start()
        {
            if (!animator) animator = GetComponent<Animator>();
            if (!sequence) sequence = GetComponent<ProgressEvent>();
        }
        
        public void Play() { Play(gameObject.name); }
        public void Play(string animation)
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(PlayRoutine(animation));
        }
        
        public void Cancel() { sequence.Cancel(); }

        IEnumerator PlayRoutine(string animation)
        {
            animator.Play(animation);
            yield return null;
            sequence.Begin(stateInfo.length);
        }
    }
}