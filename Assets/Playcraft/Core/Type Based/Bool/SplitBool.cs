using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class SplitBool : MonoBehaviour
    {
        [SerializeField] UnityEvent OnTrue;
        [SerializeField] UnityEvent OnFalse;
        
        public bool state;
        public void SetState(bool value) { state = value; }
        
        public void Input(bool value)
        {
            state = value;
            Output();
        }   
        
        public void Toggle(bool outputState)
        {
            state = !state;
            if (outputState) Output();     
        }
        
        public void Output()
        {
            if (state) OnTrue.Invoke();
            else OnFalse.Invoke();            
        }
    }
}
