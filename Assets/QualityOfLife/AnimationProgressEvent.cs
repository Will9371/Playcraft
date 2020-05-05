using System.Collections;
using UnityEngine;

// REFACTOR: merge into AnimationPicker -> AnimatorInterface
public class AnimationProgressEvent : MonoBehaviour 
{
    [SerializeField] ProgressEvent sequence;
    [SerializeField] Animator animator;
    
    public bool completed { get { return percent >= 1f; } }
    float percent { get { return stateInfo.normalizedTime; } }
    AnimatorStateInfo stateInfo { get { return animator.GetCurrentAnimatorStateInfo(0); } }
    
    public void Play(string animation)
    {
        StartCoroutine(PlayRoutine(animation));
    }
    
    public void Cancel()
    {
        sequence.Cancel();
    }

    IEnumerator PlayRoutine(string animation)
    { 
        animator.Play(animation);
        yield return null;
        sequence.Begin(stateInfo.length);
    }
}
