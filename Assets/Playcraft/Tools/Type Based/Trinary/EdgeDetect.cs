using UnityEngine;

public class EdgeDetect : MonoBehaviour
{
    enum NormalState { False, True, Unchanged }

    [SerializeField] NormalState defaultState;
    [SerializeField] BoolEvent OnEdgeDetect;
    
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
            case NormalState.False: state = false; break;
            case NormalState.True: state = true; break;
            case NormalState.Unchanged: break;
        }
    }
}
