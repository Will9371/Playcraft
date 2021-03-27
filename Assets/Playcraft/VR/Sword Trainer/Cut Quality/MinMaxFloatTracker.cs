using UnityEngine;
using UnityEngine.Events;

public class MinMaxFloatTracker : MonoBehaviour
{
    [SerializeField] Vector2 range;
    [SerializeField] bool maxOnStart;
    [SerializeField] FloatEvent OnChangeValue;
    [SerializeField] UnityEvent OnReachMinimum;
    
        
    float _value;
    public float Value
    {
        get => _value;
        set
        {
            if (_value <= 0)
                return;
            
            _value = value;
            
            if (value <= 0)
            {
                _value = 0;
                OnReachMinimum.Invoke();
            }
            
            OnChangeValue.Invoke(_value);
        }
    }
    
    void Start() { if (maxOnStart) SetToMax(); }
    
    public void SetToMax() 
    {
        _value = range.y; 
        Value = range.y; 
    }
    
    public void Subtract(float value) { Value -= value; }
}
