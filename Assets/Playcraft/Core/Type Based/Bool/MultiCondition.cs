using System;
using System.Collections.Generic;
using UnityEngine;


namespace Playcraft
{
    [Serializable]
    public class MultiCondition
    {
        [SerializeField] GameObject[] observations;
        [SerializeField] bool requireAll;
        [SerializeField] bool requiredState;

        List<IBool> Observations = new List<IBool>();
        
        public void SetObservations()
        {
            foreach (var obj in observations)
            {
                var _observation = obj.GetComponent<IBool>();
                if (_observation == null) continue;
                Observations.Add(_observation);
            }
        }
        
        public void SetObservations(IBool[] values)
        {
            foreach (var value in values)
                Observations.Add(value);
        }

        public bool IsConditionMet()
        {
            var result = requireAll;
            
            if (Observations.Count == 0)
                Debug.LogError("Checking condition against 0 observations");

            foreach (var item in Observations)
            {
                if (requireAll == (item.State == requiredState))
                    continue;

                result = !requireAll;           
            }

            return result; 
        }
    }
}