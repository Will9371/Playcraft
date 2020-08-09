using System;
using UnityEngine;

public class LocalStateHub : MonoBehaviour
{
    TagSO priorState;
    public TagSO state;    // For debug
    public Action<TagSO> OnStateEnter;
    
    public void SetState(TagSO value)
    {
        priorState = state == null ? value : state;
        state = value;
        OnStateEnter.Invoke(value);
    }
    
    public void ReturnToPriorState()
    {
        SetState(priorState);
    }
}
