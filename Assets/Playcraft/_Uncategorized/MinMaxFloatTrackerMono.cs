using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class MinMaxFloatTrackerMono : MonoBehaviour
    {
        [SerializeField] MinMaxFloatTracker process;
        [SerializeField] FloatEvent OnChangeValue;
        [SerializeField] UnityEvent OnReachMinimum;
        
        enum AutoInitialization { NA, Start, Enable }
        [SerializeField] AutoInitialization autoInitialization;

        float max => process.max;

        void Start() 
        { 
            if (autoInitialization == AutoInitialization.Start)
                SetToMax();
        }
        
        void OnEnable()
        {
            if (autoInitialization == AutoInitialization.Enable)
                SetToMax();
        }
        
        public void SetToMax()
        {
            process.SetToMax();
            OnChangeValue.Invoke(max);
        }
        
        public void ResetMax(float value) 
        { 
            process.ResetMax(value); 
            OnChangeValue.Invoke(max);
        }
        
        public void Subtract(float value)
        {
            var (newValue, isMinimum) = process.Subtract(value);
            OnChangeValue.Invoke(newValue);
            if (isMinimum) OnReachMinimum.Invoke();
        }
    }
}
