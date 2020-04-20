using System;
using UnityEngine;

public class HumanoidAnimationInterface : MonoBehaviour
{
    Animator animator;
    MoveData moveData;
    
    [SerializeField] [Range(0f, 1f)] float defaultCrossFade = .3f;

    //[SerializeField] AnimationClip jumpClip;

        
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
        var state = moveData.state.animations;
        var clip = state.GetClip(moveData.rotation);
        Refresh(clip.name);
    }
    
    private void Refresh(string currentAnimation)
    {  
        if (currentAnimation == priorAnimation)
            return;

        animator.CrossFade(currentAnimation, defaultCrossFade, 0);
        priorAnimation = currentAnimation;
    }
}