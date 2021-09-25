using System;
using UnityEngine;

// RENAME
namespace Playcraft.Examples.SwordTrainer
{
    [Serializable] 
    public class SwordModeData
    {
        // * Calculate from querying ISwordAction
        // Get type info -> refresh lists
        public int cutsActive;
        public int parriesActive;
        public bool simultaneous;
        
        [SerializeField] GameObject[] actionContainers;
        ISwordAction[] actions; 
        int actionIndex;

        public void Initialize()
        {
            actions = new ISwordAction[actionContainers.Length];
            
            for (int i = 0; i < actionContainers.Length; i++)
                actions[i] = actionContainers[i].GetComponent<ISwordAction>();
        }
        
        public void CycleAction()
        {
            if (actionIndex >= actions.Length)
                actionIndex = 0;
        
            actions[actionIndex].Trigger();
            actionIndex++;
        }
        
        public void TriggerAll()
        {
            foreach (var action in actions)
                action.Trigger();
        }
        
        public bool AnyActive()
        {
            foreach (var action in actions)
                if (action.hittable)
                    return true;
                    
            return false;
        }
    }
}