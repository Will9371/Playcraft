using System;
using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class SimpleEventListenerMono : MonoBehaviour
    {
        [SerializeField] SimpleEventListener process;
        [SerializeField] UnityEvent response;
        
        void OnEnable() { process.AddTrigger(Trigger); }
        void OnDisable() { process.RemoveTrigger(Trigger); }
        
        void Trigger() { response.Invoke(); }
    }

    [Serializable]
    public class SimpleEventListener
    {
        public EventSO id;
        
        public void AddTrigger(Action action) { id.onSimple += action; }
        public void RemoveTrigger(Action action) { id.onSimple -= action; }
    }
}