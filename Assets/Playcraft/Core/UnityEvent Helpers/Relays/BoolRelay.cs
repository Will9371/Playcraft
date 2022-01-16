using UnityEngine;

public class BoolRelay : MonoBehaviour
{
    [SerializeField] BoolEvent Output = default;
    
    public void Input(bool value) 
    { 
        Output.Invoke(value); 
        priorValue = value;
    }
    
    bool priorValue;
    
    public void RelayIfChanged(bool value) 
    {
        if (value != priorValue)
            Output.Invoke(value);
            
        priorValue = value;
    } 
}
