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
            SetMode();
        }
        
        public void Cycle()
        {
            index++;
            
            if (index >= stages.Length)
                index = 0;
                
            SetMode();
        }
        
        void SetMode()
        {
            fighter.RequestSetMode(stages[index].mode);
            SetSlowTimeActive(stages[index].slowTime);
        }
        
        public void SetSlowTimeActive(bool value)
        {
            slowTimeActive = value;
            trackMovements.enabled = value;
            if (!value) slowTime.SetDefault();
        }
        
        public void ToggleSlowTimeActive() { SetSlowTimeActive(!slowTimeActive); }
        
        [Serializable]
        public struct Stage
        {
            public SwordModeId mode;
            public bool slowTime;
        }
    }
}