using System;
using System.Collections;
using UnityEngine;

// * Eliminate Cut/Parry specific logic, apply uniform SwordAction interface instead
// * Simultaneous: apply all actions on enter, repeat indexed action on complete
// * Alternating: cycle action on enter and when any action completes
// -> Redirect methods from Cut/ParryInstance to currentMode
namespace Playcraft.Examples.SwordTrainer
{
    public class SwordTrainer : MonoBehaviour
    {
        #region Modes
        
        [SerializeField] StringEvent sendModeId;
        
        SwordModeId currentModeId = SwordModeId.Inactive;
        SwordModeId priorModeId = SwordModeId.Inactive;
        SwordModeData currentMode;
        bool initialized;
        bool changingMode;

        [Serializable] class SwordModeDictionary : SerializableDictionary<SwordModeId, SwordModeData> { }
        [SerializeField] SwordModeDictionary lookup;

        void Awake()
        {
            foreach (var element in lookup)
                element.Value.Initialize();
        }

        public void RequestSetMode(SwordModeId value)
        {
            currentModeId = value;
            RequestSetMode();
        }
        
        void RequestSetMode() 
        {
            if (priorModeId == currentModeId && initialized)
                return;

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
            SetCutBarriersActive();
            
            if (currentMode.simultaneous)
                currentMode.TriggerAll();
            else
                CycleAction();
            
            sendModeId.Invoke(currentModeId.ToString());
        }
        
        void ActivateTargets() { ActivateCuts(); ActivateParries(); }
        
        void CycleAction() { StartCoroutine(WaitToCycleAction()); }
        
        IEnumerator WaitToCycleAction()
        {
            while (currentMode.AnyActive())
                yield return null;
                
            currentMode.CycleAction();
        }      
        
        #endregion

        #region Cuts
        
        [SerializeField] CutInstance[] cutInstances;
        
        void ActivateCuts()
        {
            for (int i = 0; i < cutInstances.Length; i++)
            {
                cutInstances[i].SetActive(i < currentMode.cutsActive);
                cutInstances[i].SetLocalPosition(i);
            }            
        }
        
        public void CutComplete(int index) 
        { 
            if (currentMode.simultaneous)
                Cut(index);
            else
                CycleAction();

            RequestSetMode();
        }
        
        void SetCutBarriersActive()
        {
            if (currentMode.cutsActive <= 0) return;
            var barriersActive = currentMode.cutsActive == 1;
            
            foreach (var instance in cutInstances)
                instance.SetBarriersActive(barriersActive);
        }
        
        void Cut(int index) 
        { 
            if (changingMode) return;
            cutInstances[index].Cut(); 
        }

        [Serializable]
        public class CutInstance
        {
            [SerializeField] CutTarget target;
            [SerializeField] Vector3[] localPositionByInstanceCount;
            
            public void SetBarriersActive(bool value) { target.SetBarriersActive(value); }
            public void SetActive(bool value) { target.SetActive(value); }
            public void Cut() { target.BeginExtension(); }
            public void SetLocalPosition(int index) { target.SetLocalPosition(localPositionByInstanceCount[index]); }
        }        
        
        #endregion

        #region Parries
        
        [SerializeField] ParryInstance[] parryInstances;
        
        void ActivateParries()
        {
            for (int i = 0; i < parryInstances.Length; i++)
            {
                parryInstances[i].SetActive(i < currentMode.parriesActive);
                parryInstances[i].SetLocalPosition(i);
            }            
        }    
        
        public void ParryComplete(int index) 
        {  
            if (currentMode.simultaneous)
                Parry(index);
            else
                CycleAction();
            
            RequestSetMode();
        }

        void Parry(int index) 
        {
            if (changingMode) return; 
            parryInstances[index].Parry(); 
        }

        [Serializable]
        public class ParryInstance
        {
            [SerializeField] SetParry target;
            [SerializeField] Vector3[] localPositionByInstanceCount;
            
            public void SetActive(bool value) { target.SetActive(value); }
            public void Parry() { target.BeginActivation(); }
            public void SetLocalPosition(int index) { target.SetLocalPosition(localPositionByInstanceCount[index]); }
        }        
        
        #endregion
    }
}

/* void SetCutTags()
{
    if (!currentMode.cutsActive) return;
    var tagGroupIndex = currentMode.doubleCutsActive ? 1 : 0;
    cutPrompt.SetTriggerTags(tagGroupIndex);
} */

