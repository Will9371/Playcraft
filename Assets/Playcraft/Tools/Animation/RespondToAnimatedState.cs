using UnityEngine;

namespace Playcraft
{
    public class RespondToAnimatedState : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] AnimatedMoveState[] respondToStates;
        [SerializeField] FloatEvent BroadcastDuration;
        #pragma warning restore 0649

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
}
