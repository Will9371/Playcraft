using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "Playcraft/Events/Tag")]
    public class StateGameEvent : GameEvent
    {
        public void Raise(SO value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(value);
        }
    }
}
