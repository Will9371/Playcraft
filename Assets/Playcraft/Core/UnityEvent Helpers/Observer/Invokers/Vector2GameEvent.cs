using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "Playcraft/Events/Vector 2")]
    public class Vector2GameEvent : GameEvent
    {
        public void Raise(Vector2 value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(value);
        }
    }
}
