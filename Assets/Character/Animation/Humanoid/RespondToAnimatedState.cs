using UnityEngine;

public class RespondToAnimatedState : MonoBehaviour
{
    [SerializeField] AnimatedMoveState[] respondToStates;
    [SerializeField] FloatEvent BroadcastDuration;

    public void CheckState(AnimatedMoveState state, float duration)
    {
        foreach (var item in respondToStates)
        {
            //Debug.Log(item + " " + state);
            if (item == state)
            {
                BroadcastDuration.Invoke(duration);
                break;
            }
        }
    }
}
