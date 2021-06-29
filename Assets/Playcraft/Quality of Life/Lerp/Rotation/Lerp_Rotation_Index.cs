using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class Lerp_Rotation_Index
    {
        public Vector3[] rotations;
        [SerializeField] Lerp_Rotation process;
        
        public int startIndex;
        public int endIndex;
        
        public void SetSelfIfNull(Transform value) { process.SetSelfIfNull(value); }
        
        public void Input(float percent) { process.Input(percent); }
        
        public void SetDestination(int newIndex) { SetEndpoints(endIndex, newIndex); }
        
        public void SetEndpoints(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            process.SetPath(rotations[startIndex], rotations[endIndex]);
        }
        
        public void CycleDestination(bool forward)
        {
            var newIndex = RangeMath.CycleInt(endIndex, rotations.Length - 1, forward);
            SetDestination(newIndex);
        }
        
        public void SetDestinations(Vector3[] values)
        { 
            rotations = new Vector3[values.Length];
                        
            for (int i = 0; i < values.Length; i++) 
                rotations[i] = values[i];
        }
    }
}
