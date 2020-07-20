﻿using UnityEngine;

public class HandAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    
    const string OPEN = "Open";
    const string CLOSE = "Close";
    
    bool isClosed;
    public void SetClosed(bool value) 
    { 
        isClosed = value;
        var animation = value ? CLOSE : OPEN;
        animator.Play(animation); 
    }
}
