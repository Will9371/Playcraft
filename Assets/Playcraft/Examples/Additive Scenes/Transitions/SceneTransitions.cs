using UnityEngine;

public class SceneTransitions : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] AnimationProgressEvent fadeOut;
    
    SceneController scene => SceneController.instance;
    
    SceneTransitionSO currentTransition;
    
    void OnEnable()
    {
        animator.Play("Fade In");        
    }
    
    public void FadeOut(SceneTransitionSO transition) 
    {
        currentTransition = transition; 
        fadeOut.Play("Fade Out"); 
    }
    
    public void FadeOutComplete()
    {
        scene.LoadUnload(currentTransition);
    }
}
