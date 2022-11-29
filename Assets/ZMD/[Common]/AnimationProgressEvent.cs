using System.Collections;
using UnityEngine;


namespace ZMD
{
    /// Extension to ProgressEvent: uses animation as source of time interval
    public class AnimationProgressEvent : MonoBehaviour 
    {
        [SerializeField] Animator animator;
        [SerializeField] ProgressEvent sequence;
        
        float percent => stateInfo.normalizedTime; 
        AnimatorStateInfo stateInfo => animator.GetCurrentAnimatorStateInfo(0); 
        
        void Start() { if (!animator) animator = GetComponent<Animator>(); }
        void OnValidate() { sequence.OnValidate(this); }
        
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
            sequence.SetDurationAndBegin(stateInfo.length);
        }
    }
}