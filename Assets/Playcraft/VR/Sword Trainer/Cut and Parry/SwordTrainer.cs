using System;
using System.Collections;
using UnityEngine;

// * Delegate Cuts & Parries logic to non-mono classes
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
                element.Value.Initialize(element.Key, this);
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
        
            while (AnyActive())
                yield return null;
                
            changingMode = false;
            SetMode();
        }
        
        void SetMode()
        {
            currentMode?.process?.Exit();
            lookup.TryGetValue(currentModeId, out currentMode);
            
            ActivateTargets();
            SetCutBarriersActive();
            currentMode?.process?.Enter();
            sendModeId.Invoke(currentModeId.ToString());
        }
        
        void ActivateTargets() { ActivateCuts(); ActivateParries(); }
        bool AnyActive() { return AnyCutActive() || AnyParryActive(); }        
        
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
            currentMode?.process?.CutComplete(index);
            RequestSetMode();
        }
        
        void SetCutBarriersActive()
        {
            if (currentMode.cutsActive <= 0) return;
            var barriersActive = currentMode.cutsActive == 1;
            
            foreach (var instance in cutInstances)
                instance.SetBarriersActive(barriersActive);
        }
        
        public void Cut(int index) 
        { 
            if (changingMode) return;
            cutInstances[index].Cut(); 
        }
        
        public void CutAll() 
        { 
            for (int i = 0; i < cutInstances.Length; i++)
                Cut(i);
        }
        
        public void CycleCut(int priorIndex)
        {
            var index = priorIndex + 1;
            
            if (index >= cutInstances.Length)
                index = 0;
                
            Cut(index);
        }
        
        bool AnyCutActive()
        {
            foreach (var instance in cutInstances)
                if (instance.active)
                    return true;
            
            return false;
        }
        
        [Serializable]
        public class CutInstance
        {
            [SerializeField] CutTarget target;
            [SerializeField] Vector3[] localPositionByInstanceCount;
            
            public bool active => target.hittable;
            
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
            currentMode?.process?.ParryComplete(index);
            RequestSetMode();
        }

        public void Parry(int index) 
        {
            if (changingMode) return; 
            parryInstances[index].Parry(); 
        }
        
        public void ParryAll()
        {
            for (int i = 0; i < parryInstances.Length; i++)
                Parry(i);
        }
        
        public void CycleParry(int priorIndex)
        {
            var index = priorIndex + 1;
            
            if (index >= parryInstances.Length)
                index = 0;
                
            Parry(index);
        }
        
        bool AnyParryActive()
        {
            foreach (var instance in parryInstances)
                if (instance.active)
                    return true;
                    
            return false;
        }
        
        [Serializable]
        public class ParryInstance
        {
            [SerializeField] SetParry target;
            [SerializeField] Vector3[] localPositionByInstanceCount;
            
            public float holdTime => target.holdTime;
            public float transitionTime => target.transitionTime;
            public bool active => target.hittable;

            public void SetActive(bool value) { target.SetActive(value); }
            public void Parry() { target.BeginActivation(); }
            public void SetLocalPosition(int index) { target.SetLocalPosition(localPositionByInstanceCount[index]); }
        }        
        
        #endregion

        #region Obsolete
        
        // For calling coroutines from non-MonoBehaviours
        public delegate IEnumerator Routine();
        public void RemoteRoutine(Routine routine) { StartCoroutine(routine()); }
        
        // Used by obsolete CutAndParryAlternatingMode.cs
        public float halfActionTime => parryInstances[0].holdTime / 2f + parryInstances[0].transitionTime;            
        
        #endregion
    }
}

/* void SetCutTags()
{
    if (!currentMode.cutsActive) return;
    var tagGroupIndex = currentMode.doubleCutsActive ? 1 : 0;
    cutPrompt.SetTriggerTags(tagGroupIndex);
} */

