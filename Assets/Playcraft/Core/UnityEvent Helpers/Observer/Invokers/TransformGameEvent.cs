using UnityEngine;

namespace Playcraft
{
    [CreateAssetMenu(fileName = "Transform Event", menuName = "Playcraft/Events/Transform")]
    public class TransformGameEvent : GameEvent
    {
        public void Raise(Transform value)
        {
            for (int i = listeners.Count - 1; i >= 0; i--)
                listeners[i].OnEventRaised(value);
        }
    }
}