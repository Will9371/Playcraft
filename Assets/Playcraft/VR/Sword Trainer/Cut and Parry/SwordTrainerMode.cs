using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public enum FightMode 
    { 
        Inactive,
        Cut, 
        Parry, 
        CutAndParry,
        CutAndParryAlternating,
        CutAndParrySimultaneous,
        DoubleCutAlternating, 
        DoubleParryAlternating,
        DoubleCutSimultaneous, 
        DoubleParrySimultaneous
    }

    [CreateAssetMenu(menuName = "Playcraft/VR/Sword Trainer/Opponent Mode")]
    public class SwordTrainerMode : ScriptableObject
    {
        public bool cutsActive => data.cutsActive;
        public bool parriesActive => data.parriesActive;
        public bool twoWeapon => data.twoWeapon;
        public bool simultaneous => data.simultaneous;
        public bool alternating => data.alternating;

        ModeData data;

        [Serializable] public class FightModeDataDictionary : SerializableDictionary<FightMode, ModeData> { }
        [SerializeField] FightModeDataDictionary modeLookup;
        
        public void SetData(FightMode id) { modeLookup.TryGetValue(id, out data); }

        public bool NextActionIncludes(FightAction requestedAction, FightAction priorAction)
        {
            switch (requestedAction)
            {
                case FightAction.Cut: return NextActionIncludes(requestedAction, data.cutsActive, priorAction);
                case FightAction.Parry: return NextActionIncludes(requestedAction, data.parriesActive, priorAction);
                default: return true;
            }
        }
        
        bool NextActionIncludes(FightAction requestedAction, bool isActive, FightAction priorAction)
        {
            if (!isActive)
                return false;
            if (twoWeapon)
                return true;
                
            var repeatAction = priorAction == requestedAction;
            var oneTypeAllowed = cutsActive != parriesActive;

            //Debug.Log($"{requestedAction} {priorAction} {oneTypeAllowed || !repeatAction} {Time.time}");
            return oneTypeAllowed || !repeatAction;       
        }
        
        [Serializable] 
        public struct ModeData
        {
            [Tooltip("Prompt cuts while in this mode")]
            public bool cutsActive;
            [Tooltip("Prompt parries while in this mode")]
            public bool parriesActive;
            [Tooltip("Two prompts can be active at the same time")]
            public bool twoWeapon;
            [Tooltip("Begin both prompts at the same time (assumes twoWeapon = true)")]
            public bool simultaneous;

            public bool alternating => twoWeapon && !simultaneous;
        }
    }
}

