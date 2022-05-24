using UnityEngine;
using ZMD;

public class ExampleResponse_B : MonoBehaviour
{
    public void InputCounter(int value) { Debug.Log(value); }

    public void InputDirection(Vector3 value) { Debug.Log(value); }
    
    public void InputDirection(Vector3SO value) { InputDirection(value.value); }
    
    #region Dependencies
    
    public void DirectlyReferenced() { Debug.Log("Direct reference successful!"); }
    
    
    [SerializeField] ExampleTrigger_A scriptA;
    void Awake() { scriptA.onStart += EventListener; }
    void OnDestroy() { scriptA.onStart -= EventListener; }
    void EventListener() { Debug.Log("Event listener successful!"); }
    
    
    #endregion
}
