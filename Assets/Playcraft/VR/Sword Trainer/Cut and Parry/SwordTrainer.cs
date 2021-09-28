using System;
using System.Collections;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwordTrainer : MonoBehaviour
    {
        [SerializeField] TargetGroup cuts;        
        [SerializeField] TargetGroup parries;
        [SerializeField] StringEvent sendModeId;
        
         [Serializable] class SwordModeDictionary : SerializableDictionary<SwordModeId, SwordModeData> { }
         [SerializeField] SwordModeDictionary lookup;       
        
        SwordModeId currentModeId = SwordModeId.Inactive;
        SwordModeId priorModeId = SwordModeId.Inactive;
        SwordModeData currentMode;
        bool initialized;
        bool changingMode;
        Stage currentStage;

        void Awake()
        {
            foreach (var element in lookup)
                element.Value.Initialize();
        }

        public void SetStage(Stage stage)
        {
            currentStage = stage;
            currentModeId = stage.mode;
            BeginSetMode();
        }
        
        void RequestSetMode() 
        {
            if (priorModeId == currentModeId && initialized)
                return;

            BeginSetMode();
        }
        
        void BeginSetMode()
        {
            initialized = true;
            priorModeId = currentModeId;
            
            StopAllCoroutines();
            StartCoroutine(WaitToSetMode());            
        }
        
        IEnumerator WaitToSetMode()
        {
            changingMode = true;
        
            while (currentMode != null && currentMode.AnyActive())
                yield return null;
                
            changingMode = false;
            SetMode();
        }
        
        void SetMode()
        {
            lookup.TryGetValue(currentModeId, out currentMode);
            
            ActivateTargets();
            cuts.RefreshSettings(currentStage.cutSettings);
            parries.RefreshSettings(currentStage.parrySettings);

            if (currentMode.simultaneous)
                currentMode.TriggerAll();
            else
                StartCoroutine(WaitToCycleAction());
            
            sendModeId.Invoke(currentModeId.ToString());
        }

        void ActivateTargets() 
        { 
            cuts.SetActive(currentMode.cutsActive);
            parries.SetActive(currentMode.parriesActive); 
        }

        IEnumerator WaitToCycleAction()
        {
            while (currentMode.AnyActive())
                yield return null;
                
            currentMode.CycleAction();
        }
        
        public void CutComplete(int index) { ActionComplete(SwordActionId.Cut, index); }
        public void ParryComplete(int index) { ActionComplete(SwordActionId.Parry, index); }
        void ActionComplete(SwordActionId actionId, int index)
        {
            if (currentMode.simultaneous)
            {
                if (changingMode) return;
                currentMode.TriggerIndexedAction(actionId, index);                
            }
            else
                StartCoroutine(WaitToCycleAction());
        
            RequestSetMode();
        }
    }
}

