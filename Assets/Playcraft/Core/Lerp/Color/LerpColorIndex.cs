using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpColorIndex
    {
        [SerializeField] int defaultIndex;
        [SerializeField] Color[] colors;
        [SerializeField] LerpColor process;
        
        int index;

        public Color start
        {
            get => process.start;
            set => process.start = value;
        }
        public Color end
        {
            get => process.end;
            set => process.end = value;
        }
        
        public void Initialize()
        {            
            if (colors.Length <= 0) return;

            index = defaultIndex;
            start = colors[defaultIndex];
            end = start;
            Input(0f);
        }
            
        // Move between internally-stored positions
        public void SetDestination(int newIndex)
        {
            if (newIndex >= colors.Length)
            {
                Debug.LogError("Attempting to set color index " + newIndex + " of " + colors.Length);
                return;
            }
            
            start = colors[index];
            end = colors[newIndex];
            index = newIndex;
        }
            
        // Move towards externally defined location
        public void SetTargetColor(Color value) { process.SetTargetColor(value); }
            
        // Call continuously to move over time
        public void Input(float percent) { process.Input(percent); }
    }
}
