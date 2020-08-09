using UnityEngine;

public class BoolRelay : MonoBehaviour
{
    [SerializeField] BoolEvent Output = default;
    
    public void Input(bool value)
    {
        Output.Invoke(value);
    }
}
