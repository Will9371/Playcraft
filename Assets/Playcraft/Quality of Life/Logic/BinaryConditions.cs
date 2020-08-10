using System;
using UnityEngine;
using UnityEngine.Events;

// RENAME
public class BinaryConditions : MonoBehaviour
{
    public enum Condition { A_Only, B_Only, OR, AND, NOR, XOR, XNOR }
    
    [SerializeField] bool triggerResponseOnConditionChange = true;
    [SerializeField] ConditionalResponse[] conditions = default;
    
    bool a;
    bool b;
    
    public void SetConditionA(bool value) 
    { 
        a = value; 
        
        if (triggerResponseOnConditionChange)
            Trigger(); 
    }
    public void SetConditionB(bool value) 
    { 
        b = value;
        
        if (triggerResponseOnConditionChange)
            Trigger(); 
    }
    
    public void Trigger() 
    {
         foreach (var condition in conditions)
         {
            bool result;
                  
            switch (condition.condition)
            {
                case Condition.A_Only: result = a && !b; break;
                case Condition.B_Only: result = !a && b; break;
                case Condition.OR: result = a || b; break;
                case Condition.AND: result = a && b; break;
                case Condition.NOR: result = !a && !b; break;
                case Condition.XOR: result = (a && !b) || (!a && b); break;
                case Condition.XNOR: result = (a && b) || (a && b); break;
                default: result = false; break;
            }
            
            if (result) condition.True.Invoke();
         }
    }
    
    [Serializable] public struct ConditionalResponse
    {
        public Condition condition;
        public UnityEvent True;
    }
}
