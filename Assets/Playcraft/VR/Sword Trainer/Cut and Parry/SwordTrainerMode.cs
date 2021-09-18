using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    [CreateAssetMenu(menuName = "Playcraft/VR/Sword Trainer/Opponent Mode")]
    public class SwordTrainerMode : ScriptableObject
    {
        enum FightMode 
        { 
            Inactive, 
            Cut, 
            Parry, 
            CutAndParryAlternating, 
            CutAndParrySimultaneous,
            DoubleCutAlternating, 
            DoubleParryAlternating,
            DoubleCutSimultaneous, 
            DoubleParrySimultaneous
        }
    
        [Tooltip("If true, bool settings will be set automatically by the mode enum.  " +
                 "If false, mode enum will be set automatically by the bool settings.")]
        [SerializeField] bool setByMode;
        
        [SerializeField] FightMode mode;
        
        public bool cutsActive;
        public bool parriesActive;
        public bool simultaneous;
        public bool twoWeapon;

        public bool NextActionIncludes(FightAction requestedAction, FightAction priorAction)
        {
            switch (requestedAction)
            {
                case FightAction.Cut: return NextActionIncludes(requestedAction, cutsActive, priorAction);
                case FightAction.Parry: return NextActionIncludes(requestedAction, parriesActive, priorAction);
                default: return true;
            }
        }
        
        bool NextActionIncludes(FightAction requestedAction, bool isActive, FightAction priorAction)
        {
            if (!isActive)
                return false;
            if (simultaneous || twoWeapon)
                return true;
                
            var repeatAction = priorAction == requestedAction;
            var oneTypeAllowed = cutsActive != parriesActive;

            //Debug.Log($"{requestedAction} {priorAction} {oneTypeAllowed || !repeatAction} {Time.time}");
            return oneTypeAllowed || !repeatAction;       
        }
        
        #region Settings Validation

        void OnValidate()
        {
            if (setByMode)
                SetSettingsByMode();
            else
                mode = SetModeBySettings();
        }
        
        void SetSettingsByMode()
        {
            switch (mode)
            {
                case FightMode.Inactive:
                    cutsActive = false;
                    parriesActive = false;
                    simultaneous = false;
                    twoWeapon = false;
                    break;
                case FightMode.Cut:
                    cutsActive = true;
                    parriesActive = false;
                    simultaneous = false;
                    twoWeapon = false;
                    break;
                case FightMode.Parry:
                    cutsActive = false;
                    parriesActive = true;
                    simultaneous = false;
                    twoWeapon = false;
                    break;
                case FightMode.CutAndParryAlternating:
                    cutsActive = true;
                    parriesActive = true;
                    simultaneous = false;
                    twoWeapon = false;
                    break;
                case FightMode.CutAndParrySimultaneous:
                    cutsActive = true;
                    parriesActive = true;
                    simultaneous = true;
                    twoWeapon = false;
                    break;
                case FightMode.DoubleCutAlternating:
                    cutsActive = true;
                    parriesActive = false;
                    simultaneous = false;
                    twoWeapon = true;
                    break;   
                case FightMode.DoubleCutSimultaneous:
                    cutsActive = true;
                    parriesActive = false;
                    simultaneous = true;
                    twoWeapon = true;
                    break;
                case FightMode.DoubleParryAlternating:
                    cutsActive = false;
                    parriesActive = true;
                    simultaneous = false;
                    twoWeapon = true;
                    break; 
                case FightMode.DoubleParrySimultaneous:
                    cutsActive = false;
                    parriesActive = true;
                    simultaneous = true;
                    twoWeapon = true;
                    break;                           
            }
        }
        
        FightMode SetModeBySettings()
        {
            if (!cutsActive && !parriesActive)
                return FightMode.Inactive;
            if (cutsActive && !parriesActive)
                return twoWeapon ? 
                    simultaneous ? 
                        FightMode.DoubleCutSimultaneous : 
                        FightMode.DoubleCutAlternating :
                    FightMode.Cut;
            if (!cutsActive && parriesActive)
                return twoWeapon ?
                    simultaneous ? 
                        FightMode.DoubleParrySimultaneous :
                        FightMode.DoubleParryAlternating : 
                    FightMode.Parry;
            if (cutsActive && parriesActive)
                return simultaneous ? FightMode.CutAndParrySimultaneous : FightMode.CutAndParryAlternating;
                
            return FightMode.Inactive;
        }
        
        #endregion
    }
}

