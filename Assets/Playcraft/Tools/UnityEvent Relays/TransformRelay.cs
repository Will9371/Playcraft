using UnityEngine;

public class TransformRelay : MonoBehaviour
{
    [SerializeField] TransformEvent Output = default;
    
    public void Input(Transform input)
    {
        Output.Invoke(input);
    }
}
