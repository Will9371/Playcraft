using UnityEngine;

public class HumanoidAnimationInterface : MonoBehaviour
{
    Animator animator;
    MoveData moveData;
    
    [SerializeField] [Range(0f, 1f)] float defaultCrossFade = .3f;
    [SerializeField] AnimationClip walkClip, idleClip;
        
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
        Refresh(GetAnimation(moveData.speed));
    }
    
    // Game specific logic -> delegate to interface or SO
    private string GetAnimation(float speed)
    {
        return speed > 0 ? walkClip.name : idleClip.name;
    }
    
    private void Refresh(string currentAnimation)
    {                
        if (currentAnimation == priorAnimation)
            return;

        animator.CrossFade(currentAnimation, defaultCrossFade);
        priorAnimation = currentAnimation;
    }
}
