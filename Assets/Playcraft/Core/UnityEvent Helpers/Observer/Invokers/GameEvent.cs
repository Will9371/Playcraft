using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    public class GameEvent : ScriptableObject
    {
        protected List<GameEventListener> listeners = new List<GameEventListener>();

        public void RegisterListener(GameEventListener listener) { listeners.Add(listener); }
        public void UnregisterListener(GameEventListener listener) { listeners.Remove(listener); }
    }
}
