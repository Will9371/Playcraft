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

        public void Initialize()
        {
            foreach (var obj in observations)
            {
                var _observation = obj.GetComponent<IBool>();
                if (_observation == null) continue;
                Observations.Add(_observation);
            }
        }

        public bool IsConditionMet()
        {
            var result = requireAll;

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