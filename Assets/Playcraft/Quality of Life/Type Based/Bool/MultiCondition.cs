using UnityEngine;

namespace Playcraft
{
    public class MultiCondition : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] ToggleState[] observations;
        [SerializeField] bool requireAll;
        [SerializeField] bool requiredState;
        [SerializeField] BoolEvent Output;
        #pragma warning restore 0649
        
        public void Refresh()
        {
            var result = requireAll;
        
            foreach (var item in observations)
            {        
                if (requireAll)
                {
                    if (item.state == requiredState)
                        continue;
                }
                else
                {
                    if (item.state != requiredState)
                        continue;
                }
                
                result = !requireAll;           
            }
            
            Output.Invoke(result);
        }
    }
}