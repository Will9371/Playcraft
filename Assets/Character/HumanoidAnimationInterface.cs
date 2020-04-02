
using UnityEngine;

public class HumanoidAnimationInterface : MonoBehaviour
{
    Animator animator;
    
    [SerializeField] float armSwingSpeed = .01f;
    [SerializeField] AnimationClip walkClip, idleClip;
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    // REFACTOR: delegate conditions, logic, and parameters to animation states
    public void SetSpeed(float speed)
    {
        var anim = speed > 0 ? walkClip.name : idleClip.name;
        animator.CrossFade(anim, .3f);
    
        if (speed > 0)
            animator.speed = armSwingSpeed * speed;
    }
}
