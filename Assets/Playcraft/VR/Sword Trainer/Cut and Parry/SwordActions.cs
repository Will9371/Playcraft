using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    [Serializable]
    public class SwordActions
    {
        [SerializeField] CutTarget cutPrompt;
        [SerializeField] SetParry parryPrompt;
        [SerializeField] SwordTrainerMode mode;
        
        FightAction state;
        
        bool cutComplete;
        bool parryComplete;
        bool cutAndParryComplete => cutComplete && parryComplete;

        public void SetMode(FightMode id) { mode.SetData(id); }
        
        public void SelectAction()
        {
            cutPrompt.gameObject.SetActive(mode.cutsActive);
            parryPrompt.gameObject.SetActive(mode.parriesActive);
            
            var canCut = mode.NextActionIncludes(FightAction.Cut, state);
            var canParry = mode.NextActionIncludes(FightAction.Parry, state);
            
            if (canParry) Parry();
            if (canCut) 
            {
                // NG: waits for cut to complete, should be continuous...reconsider design...
                var cutDelay = mode.alternating ? parryPrompt.holdTime / 2f : 0f;
                MonoSim.instance.SimInvoke(Cut, cutDelay);
            }
        }

        void Cut()
        {
            cutComplete = false;
            state = FightAction.Cut;
            cutPrompt.BeginExtension();
        }
        
        void Parry()
        {
            parryComplete = false;
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
            parryPrompt.SetRandomParry(false); 
        }
        
        public void CutComplete() 
        {
            cutComplete = true;

            if (!mode.twoWeapon || cutAndParryComplete)
                SelectAction();
        }
        
        public void ParryComplete() 
        { 
            parryComplete = true;

            if (!mode.twoWeapon || cutAndParryComplete)
                SelectAction();
        }
    }
}