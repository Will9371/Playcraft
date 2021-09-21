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
}
