using System;
using UnityEngine;

public class HumanoidAnimationInterface : MonoBehaviour
{
    Animator animator;
    MoveData moveData;
    
    [SerializeField] [Range(0f, 1f)] float defaultCrossFade = .3f;

    [SerializeField] AnimatedMoveState[] moveStates;
    [SerializeField] AnimationClip jumpClip;

        
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
        
        foreach (var state in moveStates)
            if (state.InRange(moveData.speed))
                return state.GetClip(moveData.rotation);
                
        Debug.Log("move clip not found!");
        return moveStates[0].forward;
    }
    
    private void Refresh(string currentAnimation)
    {                
        if (currentAnimation == priorAnimation)
            return;

        animator.CrossFade(currentAnimation, defaultCrossFade);
        priorAnimation = currentAnimation;
    }
}

[Serializable]
public class AnimatedMoveState
{
    public Vector2 speedRange;
    public AnimationClip forward, turnLeft, turnRight;
    
    public bool InRange(float speed)
    {
        return speed >= speedRange.x && speed <= speedRange.y;
    }
    
    public AnimationClip GetClip(float rotation)
    {
        if (rotation > 0.5f)
            return turnRight;
        if (rotation < -0.5f)
            return turnLeft;
        
        return forward;
    }
}