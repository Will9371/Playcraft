using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "Playcraft/Events/Float")]
    public class FloatGameEvent : GameEvent
    {
        public void Raise(float value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(value);
        }
    }
}
