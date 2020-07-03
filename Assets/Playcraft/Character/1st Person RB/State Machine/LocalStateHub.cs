using System;
using UnityEngine;

public class LocalStateHub : MonoBehaviour
{
    public TagSO state;    // For debug
    public Action<TagSO> OnStateEnter;
    
    public void SetState(TagSO value)
    {
        state = value;
        OnStateEnter.Invoke(value);
    }
}
