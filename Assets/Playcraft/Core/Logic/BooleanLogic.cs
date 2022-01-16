using System;
using UnityEngine;
using UnityEngine.Events;
using Playcraft;

// CONSIDER REMOVE: overly complex
public class BooleanLogic : MonoBehaviour
{
    [SerializeField] bool evaluateOnSetVariable = true;
    [SerializeField] Expression[] expressions = default;
    
    public void SetVariableTrue(SO id) { SetVariable(id, true); }
    public void SetVariableFalse(SO id) { SetVariable(id, false); }
    public void SetVariable(SO id, bool value)
    {
        foreach (var expression in expressions)
            expression.SetVariable(id, value);
                        
        if (evaluateOnSetVariable)
            Evaluate();
    }
    
    public void Evaluate()
    {        
        foreach (var expression in expressions)
            expression.Evaluate();
    }
    
    [Serializable] public class Expression
    {
        public OrBlock[] orBlocks;
        public UnityEvent True;
        
        public void SetVariable(SO id, bool value)
        {
            foreach (var block in orBlocks)
                block.SetVariable(id, value);
        }
        
        public void Evaluate()
        {
            bool result = false;
        
            foreach (var block in orBlocks)
                if (block.Evaluate())
                    result = true;
                    
            if (result) True.Invoke();
        }
        
        [Serializable] public class OrBlock
        {
            public Variable[] andBlock;
        
            public void SetVariable(SO id, bool value)
            {
                foreach (var variable in andBlock)
                    if (variable.id == id)
                        variable.SetVariable(value);
            }
        
            public bool Evaluate()
            {
                bool result = true;
        
                foreach (var variable in andBlock)
                    if (!variable.Evaluate())
                        result = false;
                    
                return result;
            }
            
            [Serializable] public class Variable
            {
                public SO id;
                public bool desired;
                public bool value;
        
                public void SetVariable(bool value)
                {
                    this.value = value;
                }
        
                public bool Evaluate()
                {
                    return desired && value || !desired && !value;
                }
            }
        }
    }
}
