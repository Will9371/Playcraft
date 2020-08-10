﻿using System;
using UnityEngine;
using UnityEngine.Events;

public class BooleanLogic : MonoBehaviour
{
    [SerializeField] bool evaluateOnSetVariable = true;
    [SerializeField] Expression[] expressions = default;
    
    public void SetVariableTrue(TagSO id) { SetVariable(id, true); }
    public void SetVariableFalse(TagSO id) { SetVariable(id, false); }
    public void SetVariable(TagSO id, bool value)
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
        
        public void SetVariable(TagSO id, bool value)
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
        
            public void SetVariable(TagSO id, bool value)
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
                public TagSO id;
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