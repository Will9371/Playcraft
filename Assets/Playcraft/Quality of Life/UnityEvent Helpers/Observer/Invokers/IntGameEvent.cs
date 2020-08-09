using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "Playcraft/Events/Int")]
    public class IntGameEvent : GameEvent
    {
        public void Raise(int value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(value);
        }
    }
}
