using System;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public interface ISwordTrainerTarget
    {
        void SetActive(int index, int activeCount);
        void SetLocalPosition(Vector3 value);
    }

    [Serializable]
    public class TargetGroup
    {
        [SerializeField] TargetInstance[] targets;

        public void SetActive(int activeCount)
        {
            for (int i = 0; i < targets.Length; i++)
            {
                targets[i].SetActive(i, activeCount);
                targets[i].SetLocalPosition(activeCount);
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

            public void SetActive(int index, int activeCount) { target.SetActive(index, activeCount); }
            
            public void SetLocalPosition(int activeCount) 
            {
                if (activeCount <= 0) return; 
                target.SetLocalPosition(localPositionByInstanceCount[activeCount-1]); 
            }
        }         
    }
}