using UnityEngine;

namespace Playcraft
{
    public class ToggleState : MonoBehaviour, IBool
    {
        bool priorState;
        public bool state;
        public bool State => state;
        [SerializeField] BoolEvent Output = default;
        
        public void Toggle(bool newState)
        {
            state = !state && newState;
        
            if (state && !priorState)
                Output.Invoke(true);
            else if (!state && priorState)
                Output.Invoke(false);
            
            priorState = state;
        }
        
        public void Toggle()
        {
            state = !state;
            Output.Invoke(state);
        }
        
        public void SetRelay(bool value)
        {
            state = value;
            Output.Invoke(state);
        }
    }
}
