using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class MultiCondition : MonoBehaviour
    {
        [SerializeField] GameObject[] observations;
        [SerializeField] bool requireAll;
        [SerializeField] bool requiredState;
        [SerializeField] BoolEvent Output;
        
        List<IBool> Observations = new List<IBool>();
        
        void Awake()
        {
            foreach (var obj in observations)
            {
                var _observation = obj.GetComponent<IBool>();
                if (_observation == null) continue;
                Observations.Add(_observation);
            }
        }
        
        public void Refresh()
        {
            var result = requireAll;
        
            foreach (var item in Observations)
            {        
                if (requireAll)
                {
                    if (item.State == requiredState)
                        continue;
                }
                else
                {
                    if (item.State != requiredState)
                        continue;
                }
                
                result = !requireAll;           
            }
            
            Output.Invoke(result);
        }
    }
}