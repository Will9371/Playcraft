using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playcraft
{
    [Serializable]
    public class LerpLocationIndex : IPercent
    {
        public Transform[] locations;
        [SerializeField] LerpLocation process;
        
        public int startIndex; 
        public int endIndex;
        
        public void SetDestination(int newIndex) { SetEndpoints(endIndex, newIndex); }
        
        public void SetRandomDestination() 
        {
            var index = Random.Range(0, locations.Length);
            SetDestination(index); 
        }
        
        void SetEndpoints(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            process.SetStartAndEnd(locations[startIndex], locations[endIndex]);
        }
        
        public void SetSelfToEnd(int endIndex)
        {
            if (endIndex >= locations.Length)
            {
                Debug.LogError($"{self.name}: index {endIndex} out of range, max is {locations.Length - 1}", self.gameObject);
                return;
            }
        
            this.endIndex = endIndex;
            process.SetSelfToEnd(locations[endIndex]);
        }
        
        public void SetDestinations(Transform[] input)
        {
            locations = new Transform[input.Length];
            
            for (int i = 0; i < input.Length; i++)
                locations[i] = input[i];
        }

        public Transform self { get => process.self; set => process.self = value; }
        public Transform start { get => process.start; set => process.start = value; }
        public Transform end { get => process.end; set => process.end = value; }
        public float percent { get => process.percent ; set => process.percent = value; }
        public void SetDestination(Transform value) { process.SetEnd(value); }
        
        public void Validate() { process.Validate(); }
    }
}