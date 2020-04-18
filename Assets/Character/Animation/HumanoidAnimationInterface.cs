
using UnityEngine;

public class HumanoidAnimationInterface : MonoBehaviour
{
    Animator animator;
    
    [SerializeField] [Range(0f, 1f)] float defaultCrossFade = .3f;
    [SerializeField] AnimationClip walkClip, idleClip;
    
    string priorAnimation;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(walkClip.ToString());
    }
    
    public void SetSpeed(float speed)
    {
        Refresh(GetAnimation(speed));
    }
    
    // Delegate to interface
    private string GetAnimation(float speed)
    {
        return speed > 0 ? walkClip.name : idleClip.name;
    }
    
    public void Refresh(string currentAnimation)
    {                
        if (currentAnimation == priorAnimation)
            return;

        animator.CrossFade(currentAnimation, defaultCrossFade);
        priorAnimation = currentAnimation;
    }
}
