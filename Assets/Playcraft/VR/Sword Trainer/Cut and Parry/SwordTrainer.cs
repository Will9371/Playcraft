using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public enum FightAction { Cut, Parry }

    public class SwordTrainer : MonoBehaviour
    {
        [SerializeField] SwordActions actions;
        [SerializeField] FightMode currentMode;

        void Start() 
        {
            actions.SetMode(currentMode);
            actions.SelectAction();
        }
        
        public void CutInProgress() { actions.CutInProgress(); }
        public void CutComplete() { actions.CutComplete(); }
        public void ParryComplete() { actions.ParryComplete(); }
        
        public void SetMode(FightMode id) 
        {
            var wasInactive = currentMode == FightMode.Inactive;
            
            actions.SetMode(id);
            currentMode = id;
            
            if (wasInactive)
                actions.SelectAction();
        }
    }
    
    [Serializable]
    public class SwordActions
    {
        [SerializeField] CutTarget cutPrompt;
        [SerializeField] SetParry parryPrompt;
        [SerializeField] SwordTrainerMode mode;
        
        FightAction state;
        
        
        public void SetMode(FightMode id) { mode.SetData(id); }
        
        public void SelectAction()
        {
            cutPrompt.gameObject.SetActive(mode.cutsActive);
            parryPrompt.gameObject.SetActive(mode.parriesActive);
            
            var canCut = mode.NextActionIncludes(FightAction.Cut, state);
            var canParry = mode.NextActionIncludes(FightAction.Parry, state);
            
            if (canCut) Cut();
            if (canParry) Parry();
        }
        
        void Cut()
        {
            state = FightAction.Cut;
            cutPrompt.BeginExtension();
        }
        
        void Parry()
        {
            state = FightAction.Parry;
            
            // Intent = move parry to random location in middle of cut
            // Error = does not randomize if player fails to cut
            //if (mode.cutsActive)
            //    parryPrompt.SetParryReady();
            //else
            parryPrompt.SetRandomParry(true);
        }
        
        public void CutInProgress() 
        {
            if (state != FightAction.Cut) return; 
            parryPrompt.SetRandomParry(false); 
        }
        
        public void CutComplete() 
        {
            if (state != FightAction.Cut) return;
            SelectAction();
        }
        
        public void ParryComplete() 
        { 
            if (state != FightAction.Parry) return;
            SelectAction();
        }
    }
}
