using UnityEngine;

public class HumanoidAnimationInterface : MonoBehaviour
{
    Animator animator;    
    [SerializeField] [Range(0f, 1f)] float defaultCrossFade = .3f;

    string animation;
    string priorAnimation;
    
    MoveState state;
    public float rotation;
    
    
    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    
    public void SetState(MoveState state)
    {
        this.state = state;
    }
    
    public void SetRotation(Vector3 rotation)
    {
        this.rotation = rotation.y;
    }
    
    private void Update()
    {
        animation = state.animations.GetClip(rotation).name;
      
        if (animation == priorAnimation)
            return;

        animator.CrossFade(animation, defaultCrossFade, 0);
        priorAnimation = animation;
    }
}