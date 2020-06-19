using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "Playcraft/Events/Float Array")]
    public class FloatArrayGameEvent : GameEvent
    {
        public void Raise(float[] value)
        {
            Debug.Log(name + " has " + listeners.Count + " listeners");
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(value);
        }
    }
}