﻿using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class RangedRepeatingTimedEvent : MonoBehaviour
    {
        [SerializeField] bool useRange = true;
        [SerializeField] Vector2 range = new(1f, 2f);
        [Tooltip("Not used if useRange set to true")]
        [SerializeField] float interval = 1f;
        [SerializeField] UnityEvent Output;
        [SerializeField] bool beginOnEnable;
        [SerializeField] bool endOnDisable;
        
        void OnEnable() { if (beginOnEnable) Begin(); }
        void OnDisable() { if (endOnDisable) End(); }
        
        public void Begin() { Invoke(nameof(Act), GetTime()); }
        public void End() { CancelInvoke(nameof(Act)); }
        
        void Act() 
        { 
            Output.Invoke();
            Invoke(nameof(Act), GetTime());
        }
        
        float GetTime() { return useRange ? Random.Range(range.x, range.y) : interval; }
        
        public void SetInterval(float value) { interval = value; }
    }
}
