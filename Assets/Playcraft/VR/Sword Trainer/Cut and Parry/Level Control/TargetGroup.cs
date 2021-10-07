using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public interface ISwordTrainerTarget
    {
        void SetActive(bool value);
        void SetLocalPosition(Vector3 value);
        void RefreshSettings(ScriptableObject value);
    }

    [Serializable]
    public class TargetGroup
    {
        [SerializeField] TargetInstance[] targets;

        public void SetActive(int activeCount)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].SetActive(i < activeCount);
                targets[i].SetLocalPosition(activeCount);
            }            
        }

        public void RefreshSettings(ScriptableObject[] settings)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                if (i >= settings.Length) break;
                targets[i].RefreshSettings(settings[i]);
            }            
        }

        [Serializable]
        public class TargetInstance
        {
            [SerializeField] GameObject container;
            [SerializeField] Vector3[] localPositionByInstanceCount;
                
            ISwordTrainerTarget target => _target ?? _Target();
            ISwordTrainerTarget _Target() { return _target = container.GetComponent<ISwordTrainerTarget>(); }
            ISwordTrainerTarget _target;

            public void SetActive(bool value) { target.SetActive(value); }
            
            public void SetLocalPosition(int activeCount) 
            {
                if (activeCount <= 0) return; 
                target.SetLocalPosition(localPositionByInstanceCount[activeCount-1]); 
            }
            
            public void RefreshSettings(ScriptableObject value) { target.RefreshSettings(value); }
        }         
    }
}