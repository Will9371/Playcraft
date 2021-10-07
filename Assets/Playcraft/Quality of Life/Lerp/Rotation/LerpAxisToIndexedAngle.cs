using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable]
    public class LerpAxisToIndexedAngle
    {
        [SerializeField] GetFloatFromArray angles;
        public RotateAxis axis;
        
        [SerializeField] int startIndex = 0;
        [SerializeField] int endIndex = 1;

        public void Input(float percent)
        {
            var angle = Mathf.Lerp(angles.values[startIndex], angles.values[endIndex], percent);
            axis.SetAngle(angle);
        }
        
        public void SetNewDestination(int newIndex)
        {
            startIndex = endIndex;
            endIndex = newIndex;
        }
        
        public void SetRandomDestination() { SetNewDestination(angles.GetRandomIndex()); }
        
        public void SetValues(FloatArray values) { angles.SetValues(values.values); }
    }
}
