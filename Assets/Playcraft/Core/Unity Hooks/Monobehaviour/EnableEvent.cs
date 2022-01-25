﻿using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class EnableEvent : MonoBehaviour
    {
        [SerializeField] UnityEvent Output = default;
        void OnEnable() { if (!locked) Output.Invoke(); }
        
        [SerializeField] bool locked;
        public void SetLocked(bool value) { locked = value; }
    }
}
