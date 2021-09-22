using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwordTrainer : MonoBehaviour
    {
        [SerializeField] CutTarget cutPrompt;
        [SerializeField] SetParry parryPrompt;
        [SerializeField] SwordModeId currentModeId;
        
        SwordModeId priorModeId = SwordModeId.Inactive;
        SwordModeData currentMode;

        [Serializable] class SwordModeDictionary : SerializableDictionary<SwordModeId, SwordModeData> { }
        [SerializeField] SwordModeDictionary lookup;
        
        public float halfActionTime => parryPrompt.holdTime / 2f + parryPrompt.transitionTime;
        
        void Awake()
        {
            foreach (var element in lookup)
                element.Value.Initialize(element.Key, this);
        }

        void Start() 
        {
            SetMode();
        }
        
        public void CutComplete() 
        { 
            currentMode.process.CutComplete();
            SetMode();
        }
        
        public void ParryComplete() 
        {  
            currentMode.process.ParryComplete();
            SetMode();
        }
        
        public void SetMode(SwordModeId value)
        {
            priorModeId = currentModeId;
            currentModeId = value;
            SetMode();
        }
        
        void SetMode() 
        {
            if (priorModeId == currentModeId)
                return;
                
            priorModeId = currentModeId;
            
            currentMode?.process.Exit();
            lookup.TryGetValue(currentModeId, out currentMode);
            
            if (currentMode == null)
                return;
            
            cutPrompt.SetActive(currentMode.settings.cutsActive);
            parryPrompt.SetActive(currentMode.settings.parriesActive);
            
            currentMode.process.Enter();
        }
        
        public void Cut() { cutPrompt.BeginExtension(); }
        public void Parry() { parryPrompt.BeginActivation(); }

        [Serializable] 
        public class SwordModeData
        {
            public SwordMode settings;
            public ISwordMode process;
            
            public void Initialize(SwordModeId id, SwordTrainer controller)
            {
                switch (id)
                {
                    case SwordModeId.Cut: process = new CutOnlyMode(controller); break;
                    case SwordModeId.Parry: process = new ParryOnlyMode(controller); break;
                    case SwordModeId.CutAndParry: process = new CutAndParryMode(controller); break;
                    case SwordModeId.CutAndParrySimultaneous: process = new CutAndParrySimultaneousMode(controller); break;
                    case SwordModeId.CutAndParryAlternating: process = new CutAndParryAlternatingMode(controller); break;
                }
            }
        }
    }
}
