using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playcraft
{
    [Serializable] public class LerpPositionIndex : IPercent
    {
        public Vector3[] positions;
        [SerializeField] LerpPosition process;
        
        public int startIndex; 
        public int endIndex;
        
        public float distance => process.distance;
        
        public void SetDestination(int newIndex) { SetEndpoints(endIndex, newIndex); }
        
        public void SetRandomDestination() 
        {
            var index = Random.Range(0, positions.Length);
            SetDestination(index); 
        }
        
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

        public Transform self { get => process.self; set => process.self = value; }
        public Vector3 start { get => process.start; set => process.start = value; }
        public Vector3 end { get => process.end; set => process.end = value; }
        public float percent { get => process.percent ; set => process.percent = value; }
        public void SetDestination(Vector3 value) { process.SetEnd(value); }
        public void SetSelfIfNull(Transform value) { process.SetSelfIfNull(value); }
    }
}