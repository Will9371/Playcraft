using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

//[CreateAssetMenu(menuName = "ZMD/Dialog/AI/NPC")]
public class DialogAINPCInfo : ScriptableObject
{
    public DialogAIState[] states;
    public DialogAINPCData Instantiate() { return new DialogAINPCData(this); }
}

[Serializable]
public class DialogAINPCData
{
    public List<DialogAIState> potentialStates;
    public List<DialogAIState> history;
    
    public DialogAINPCData(DialogAINPCInfo template)
    {
        potentialStates = new List<DialogAIState>();
        history = new List<DialogAIState>();

        foreach (var state in template.states)
            potentialStates.Add(state);
    }
    
    public DialogAIState GetState()
    {
        var allowedStates = new List<DialogAIState>();
        
        // Filter potential states by prerequisites
        foreach (var potentialState in potentialStates)
        {
            var prerequisitesMet = true;
            foreach (var prerequisite in potentialState.prerequisites)
                if (!history.Contains(prerequisite))
                    prerequisitesMet = false;
                    
            if (prerequisitesMet)
                allowedStates.Add(potentialState); 
        }
        
        if (allowedStates.Count == 0)
            return null;
    
        var index = Random.Range(0, allowedStates.Count);
        var state = allowedStates[index];
        
        history.Add(state);
        potentialStates.Remove(state);
        
        return state;
    }
}
