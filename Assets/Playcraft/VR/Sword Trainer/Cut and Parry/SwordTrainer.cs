using System;
using System.Collections;
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
            SetMode(true);
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
        
        void SetMode(bool justEnabled = false) 
        {
            if (priorModeId == currentModeId && !justEnabled) 
                return;
            
            priorModeId = currentModeId;
            currentMode?.process?.Exit();
            
            lookup.TryGetValue(currentModeId, out currentMode);
            
            ActivateTargets();
            SetCutTags();
            currentMode?.process?.Enter();
        }
        
        void ActivateTargets()
        {
            var cutXSpread = currentMode.doubleCutsActive ? doubleCutXSpread : 0f;
            cutPrompt.SetActive(currentMode.cutsActive, cutXSpread);
            cutPrompt2.SetActive(currentMode.doubleCutsActive, -cutXSpread);

            var parryXSpread = currentMode.doubleParriesActive ? doubleParryXSpread : 0f;
            parryPrompt.SetActive(currentMode.parriesActive, parryXSpread);
            parryPrompt2.SetActive(currentMode.doubleParriesActive, -parryXSpread);
        }
        
        void SetCutTags()
        {
            if (!currentMode.cutsActive) return;
            var tagGroupIndex = currentMode.doubleCutsActive ? 1 : 0;
            cutPrompt.SetTriggerTags(tagGroupIndex);
        }
        
        public void Cut() { cutPrompt.BeginExtension(); }
        public void Cut2() { cutPrompt2.BeginExtension(); }
        public void Parry() { parryPrompt.BeginActivation(); }
        public void Parry2() { parryPrompt2.BeginActivation(); }
        
        public delegate IEnumerator Routine();
        public void RemoteRoutine(Routine routine) { StartCoroutine(routine()); }
    }
}
