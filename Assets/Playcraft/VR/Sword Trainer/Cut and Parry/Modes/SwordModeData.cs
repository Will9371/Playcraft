using System;
using System.Collections.Generic;
using UnityEngine;

// RENAME
namespace Playcraft.Examples.SwordTrainer
{
    [Serializable] 
    public class SwordModeData
    {
        public int cutsActive => cutActions.Count;
        public int parriesActive => parryActions.Count;
        public bool simultaneous;
        
        [SerializeField] GameObject[] actionContainers;
        ISwordAction[] actions; 
        int actionIndex;
        List<ISwordAction> cutActions = new List<ISwordAction>();
        List<ISwordAction> parryActions = new List<ISwordAction>();

        public void Initialize()
        {
            actions = new ISwordAction[actionContainers.Length];
            cutActions.Clear();
            
            for (int i = 0; i < actionContainers.Length; i++)
            {
                actions[i] = actionContainers[i].GetComponent<ISwordAction>();
                var list = GetActionList(actions[i].actionId);
                list.Add(actions[i]);
            }
        }
        
        public void TriggerIndexedAction(SwordActionId actionId, int index)
        {
            var list = GetActionList(actionId);
            if (index >= list.Count) return;
            list[index].Trigger();
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
        
        List<ISwordAction> GetActionList(SwordActionId id)
        {
            switch (id)
            {
                case SwordActionId.Cut: return cutActions;
                case SwordActionId.Parry: return parryActions;
                default: Debug.LogError($"Invalid action {id}"); return null;
            }
        }
    }
}