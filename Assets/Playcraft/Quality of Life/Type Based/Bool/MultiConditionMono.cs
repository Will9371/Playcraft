﻿using UnityEngine;

namespace Playcraft
{
    public class MultiConditionMono : MonoBehaviour
    {
        [SerializeField] MultiCondition condition;
        [SerializeField] BoolEvent Output;
        void Awake() { condition.Initialize(); }
        public void Refresh() { Output.Invoke(condition.IsConditionMet()); }
    }
}