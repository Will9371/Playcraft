using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwordTrainer : MonoBehaviour
    {
        [Header("References")]
        [SerializeField] CutTarget cutPrompt;
        [SerializeField] CutTarget cutPrompt2;
        [SerializeField] SetParry parryPrompt;
        [SerializeField] SetParry parryPrompt2;
        
        [Header("Settings")]
        [SerializeField] float doubleCutXSpread = 0.333f;
        [SerializeField] float doubleParryXSpread = 0.333f;
        
        [Header("Testing")]
        [SerializeField] SwordModeId currentModeId;

        
        SwordModeId priorModeId = SwordModeId.Inactive;
        
        SwordModeData currentMode;
        SwordMode settings => currentMode.settings;

        [Serializable] class SwordModeDictionary : SerializableDictionary<SwordModeId, SwordModeData> { }
        [SerializeField] SwordModeDictionary lookup;
        
        public float halfActionTime => parryPrompt.holdTime / 2f + parryPrompt.transitionTime;
        
        void Awake()
        {
            foreach (var element in lookup)
                element.Value.Initialize(element.Key, this);
        }

        void OnEnable() 
        {
            SetMode();
        }
        
        public void CutComplete() 
        { 
            currentMode?.process.CutComplete();
            SetMode();
        }
        
        public void ParryComplete() 
        {  
            currentMode?.process.ParryComplete();
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
            if (currentModeId == SwordModeId.Inactive)
            {
                cutPrompt.SetActive(false);
                parryPrompt.SetActive(false);
                priorModeId = currentModeId;
                return;
            }
        
            if (priorModeId == currentModeId) return;

            priorModeId = currentModeId;
            currentMode?.process.Exit();
            
            lookup.TryGetValue(currentModeId, out currentMode);
            if (currentMode == null) return;
            
            var cutXSpread = settings.doubleCutsActive ? doubleCutXSpread : 0f;
            cutPrompt.SetActive(settings.cutsActive, cutXSpread);
            cutPrompt2.SetActive(settings.doubleCutsActive, -cutXSpread);
            
            var parryXSpread = settings.doubleParriesActive ? doubleParryXSpread : 0f;
            parryPrompt.SetActive(settings.parriesActive, parryXSpread);
            parryPrompt2.SetActive(settings.doubleParriesActive, -parryXSpread);

            currentMode.process.Enter();
        }
        
        public void Cut() { cutPrompt.BeginExtension(); }
        public void Cut2() { cutPrompt2.BeginExtension(); }
        public void Parry() { parryPrompt.BeginActivation(); }
        public void Parry2() { parryPrompt2.BeginActivation(); }

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
                    case SwordModeId.DoubleParrySimultaneous: process = new DoubleParrySimultaneousMode(controller); break;
                    case SwordModeId.DoubleParryAlternating: process = new DoubleParryAlternatingMode(controller); break;
                    case SwordModeId.DoubleCutSimultaneous: process = new DoubleCutSimultaneousMode(controller); break;
                    case SwordModeId.DoubleCutAlternating: process = new DoubleCutAlternatingMode(controller); break;
                }
            }
        }
    }
}
