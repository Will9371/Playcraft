using Playcraft;
using UnityEngine;

public class MultiCondition : MonoBehaviour
{
    [SerializeField] ToggleState[] observations;
    [SerializeField] bool requireAll;
    [SerializeField] bool requiredState;
    [SerializeField] BoolEvent Output;
    
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
