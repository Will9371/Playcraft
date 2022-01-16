﻿using UnityEngine;

namespace Playcraft
{
    public class ValidatePercent : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] float percent;
        [SerializeField] FloatEvent Output;
        
        float priorPercent;

        void OnValidate() { CheckPercent(); }    
        void Update() { CheckPercent(); }
        
        void CheckPercent()
        {
            if (percent == priorPercent) return;
            Output.Invoke(percent);
            priorPercent = percent;
        }
        
        public void ZeroPercent() { SetPercent(0); }
        public void SetPercent(float value)
        {
            percent = value;
            CheckPercent();
        }
    }
}
