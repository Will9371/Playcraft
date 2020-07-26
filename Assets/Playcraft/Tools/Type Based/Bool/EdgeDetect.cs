/*using Playcraft;
using UnityEngine;

public class EdgeDetect : MonoBehaviour
{
    [SerializeField] Trinary defaultState;
    [SerializeField] BoolEvent OnEdgeDetect;
    
    bool priorState;
    bool state;

    public void SetState(bool state) { this.state = state; }

    void Update()
    {
        if (state != priorState)
            OnEdgeDetect.Invoke(state);
        
        priorState = state;
        
        if (defaultState == Trinary.Unknown) return;
        state = defaultState == Trinary.True ? true : false;
    }
}
*/