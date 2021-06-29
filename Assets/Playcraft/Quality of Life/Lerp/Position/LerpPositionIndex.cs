using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpPositionIndex
    {
        public Vector3[] positions;
        [SerializeField] LerpPosition process;
        
        public int startIndex; 
        public int endIndex;
        
        public void SetDestination(int newIndex) { SetEndpoints(endIndex, newIndex); }
        
        void SetEndpoints(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            process.SetPath(positions[startIndex], positions[endIndex]);
        }
        
        public void SetDestinations(Vector3Array input) { SetDestinations(input.values); }
        public void SetDestinations(Vector3[] input)
        {
            positions = new Vector3[input.Length];
            
            for (int i = 0; i < input.Length; i++)
                positions[i] = input[i];
        }
        
        #region Pass to Lerp_Position
        
        public Transform self
        {
            get => process.self;
            set => process.self = value;
        }

        public Vector3 start
        {
            get => process.start;
            set => process.start = value;
        }
        
        public Vector3 end
        {
            get => process.end;
            set => process.end = value;
        }
        
        public void Input(float percent) { process.Input(percent); }
        
        public void SetDestination(Vector3 value) { process.SetEnd(value); }
        
        public void SetSelfIfNull(Transform value) { process.SetSelfIfNull(value); }
        
        #endregion
    }
}