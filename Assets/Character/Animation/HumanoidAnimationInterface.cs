using UnityEngine;

public class HumanoidAnimationInterface : MonoBehaviour
{
    Animator animator;
    MoveData moveData;
    
    [SerializeField] [Range(0f, 1f)] float defaultCrossFade = .3f;
    [SerializeField] AnimationClip idleClip, walkClip, runClip, jumpClip;
    [SerializeField] float runSpeed;
        
    string priorAnimation;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void SetMoveData(MoveData moveData)
    {
        this.moveData = moveData;
    }
    
    private void Update()
    {
        Refresh(GetAnimation(moveData.speed).name);
    }
    
    // Game specific logic -> delegate to interface or SO
    private AnimationClip GetAnimation(float speed)
    {
        if (!moveData.grounded)
            return jumpClip;
        if (speed >= runSpeed)
            return runClip;
        if (speed > 0)
            return walkClip;
            
        return idleClip;
    }
    
    private void Refresh(string currentAnimation)
    {                
        if (currentAnimation == priorAnimation)
            return;

        animator.CrossFade(currentAnimation, defaultCrossFade);
        priorAnimation = currentAnimation;
    }
}
