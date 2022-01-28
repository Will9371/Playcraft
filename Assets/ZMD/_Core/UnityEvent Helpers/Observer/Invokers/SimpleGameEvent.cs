using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "ZMD/Events/Simple")]
    public class SimpleGameEvent : GameEvent
    {
        public void Raise()
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised();
        }
    }
}
