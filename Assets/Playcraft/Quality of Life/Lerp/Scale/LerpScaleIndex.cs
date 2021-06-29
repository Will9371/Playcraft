using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpScaleIndex
    {
        [SerializeField] LerpScale process;

        public Transform self;
        public int defaultIndex;
        public Vector3[] scales;

        int index;
        
        public void SetSelfIfNull(Transform value) { process.SetSelfIfNull(value); }
        
        public void Initialize()
        {
            var arrayDefined = scales.Length > 0;
            if (arrayDefined) index = defaultIndex;
            process.start = arrayDefined ? scales[defaultIndex] : self.localScale;
        }
        
        public void SetScaleIndex(int newIndex)
        {
            process.start = scales[index];
            process.end = scales[newIndex];
            index = newIndex;
        }
            
        public void SetScale(Vector3 scale) { process.SetNewScale(scale); }

        /// Call continuously to move over time
        public void Input(float percent) { process.Input(percent); }
    }
}
