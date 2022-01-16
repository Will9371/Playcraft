using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class FlashColorArray
    {
        public Color flashColor;
        public float cycleTime;
        public int defaultFlashCount;
        public FlashColor[] values;
        
        public void Start()
        {
            foreach (var value in values)
                value.Start();
        }
        
        public void SetColor(Color color)
        {
            foreach (var value in values)
                value.SetColor(color);
        }
        
        public void Flash(MonoBehaviour mono) { Flash(mono, defaultFlashCount); }
        
        public void Flash(MonoBehaviour mono, int flashCount) 
        { 
            foreach (var value in values)
                mono.StartCoroutine(value.Flash(flashCount)); 
        }
        
        public void OnValidate()
        {
            foreach (var value in values)
            {
                value.flashColor = flashColor;
                value.cycleTime = cycleTime;
                value.defaultFlashCount = defaultFlashCount;
            }
        }
    }
}
