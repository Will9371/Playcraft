using UnityEngine;

namespace ZMD
{
    [CreateAssetMenu(fileName = "Game Event", menuName = "ZMD/Events/Float Array")]
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