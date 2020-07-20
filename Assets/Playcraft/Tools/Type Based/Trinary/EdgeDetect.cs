using UnityEngine;

namespace Playcraft
{
    public class EdgeDetect : MonoBehaviour
    {
        [SerializeField] Trinary defaultState = default;
        [SerializeField] BoolEvent OnEdgeDetect = default;
        
        bool priorState;
        bool state;
        public void SetState(bool state) { this.state = state; }

        void Update()
        {
            if (state != priorState)
                OnEdgeDetect.Invoke(state);
            
            priorState = state;
            
            switch (defaultState)
            {
                case Trinary.False: state = false; break;
                case Trinary.True: state = true; break;
                case Trinary.Unknown: break;
            }
        }
    }
}
