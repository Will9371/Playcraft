using System;
using System.Linq;
using UnityEngine.Events;

// NOT SUCCESSFULLY USED

// Input: desired next state
// Process: check whether next state is valid, given current state
// Returns: next state if success, current state if fail
[Serializable] public class StateMachine
{
    public TagSO current;
    public Transition[] transitions;
    
    public void ActOnNextIfValid(TagSO next)
    {
        if (IsValidTransition(next))
            ActOnCurrent();
    }
    
    public bool IsValidTransition(TagSO next)
    {
        var transition = GetTransition();
        if (transition == null) return false;
        var success = transition.options.Contains(next);
        if (success) current = next;
        return success;
    }
    
    public void ActOnCurrent()
    {
        var response = GetTransition();
        response?.Response.Invoke(); 
    }
    
    Transition GetTransition()
    {
        foreach (var transition in transitions)
            if (transition.state == current)
                return transition; 
                
        return null;
    }
    
    [Serializable] public class Transition
    {
        public TagSO state;
        public TagSO[] options;
        public UnityEvent Response;
    }
}
