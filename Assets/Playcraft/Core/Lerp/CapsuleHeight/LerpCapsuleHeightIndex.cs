using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpCapsuleHeightIndex 
    {
        [SerializeField] int defaultIndex;
        [SerializeField] float[] heights;
        
        [SerializeField] LerpCapsuleHeight process;
        
        int index;
            
        float start
        {
            get => process.start;
            set => process.start = value;
        }
        
        float end
        {
            get => process.end;
            set => process.end = value;
        }
        
        public void SetCapsuleIfNull(CapsuleCollider value) { process.SetCapsuleIfNull(value); }
            
        public void Initialize()
        {
            index = defaultIndex;
            start = heights[defaultIndex];
        }
        
        public void SetScaleIndex(int newIndex)
        {
            start = heights[index];
            end = heights[newIndex];
            index = newIndex;
        }

        public void Input(float percent) { process.Input(percent); }
    }
}
