using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class FightModeSequence : MonoBehaviour
    {
        [SerializeField] int startIndex;
        [SerializeField] Stage[] stages;
        [SerializeField] SwordTrainer fighter;
        [SerializeField] TrackMovements trackMovements;
        [SerializeField] SlowTime slowTime;
        
        int index;
        bool slowTimeActive;
        
        void Start()
        {
            index = startIndex;
            RefreshStage();
        }
        
        public void Cycle()
        {
            index++;
            
            if (index >= stages.Length)
                index = 0;
                
            RefreshStage();
        }
        
        void RefreshStage()
        {
            fighter.SetStage(stages[index]);
            SetSlowTimeActive(stages[index].slowTime);
        }
        
        public void SetSlowTimeActive(bool value)
        {
            slowTimeActive = value;
            trackMovements.enabled = value;
            if (!value) slowTime.SetDefault();
        }
        
        public void ToggleSlowTimeActive() { SetSlowTimeActive(!slowTimeActive); }
        
        public void ChangeTargetSettings(CutTargetSettings[] cutSettings, ParryTargetSettings[] parrySettings)
        {
            stages[index].cutSettings = cutSettings;
            stages[index].parrySettings = parrySettings;
            RefreshStage();
        }
    }
    
    [Serializable]
    public struct Stage
    {
        public SwordModeId mode;
        public CutTargetSettings[] cutSettings;
        public ParryTargetSettings[] parrySettings;
        public bool slowTime;
    }
}