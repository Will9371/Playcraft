using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace ZMD
{
    [Serializable]
    public class LerpLocationIndex : IPercent
    {
        [SerializeField] LerpLocation process;
        public Location[] locations;

        public int startIndex; 
        public int endIndex;
        
        public LerpLocationData data { set => process.data = value; }

        public void SetDestination(int newIndex) { SetEndpoints(endIndex, newIndex); }
        
        public void SetRandomDestination() { SetDestination(GetRandomDestination()); }
        public int GetRandomDestination() { return Random.Range(0, locations.Length); }
        
        public void SetEndpoints(int startIndex, int endIndex)
        {
            //Debug.Log($"Setting endpoints {startIndex} {endIndex}");
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            process.SetStart(locations[startIndex]);
            process.SetEnd(locations[endIndex]);
        }
        
        public void SetSelfToEnd(int endIndex)
        {
            if (endIndex >= locations.Length || endIndex < 0)
            {
                //Debug.LogError($"{self.name}: index {endIndex} out of range, max is {locations.Length - 1}", self.gameObject);
                return;
            }

            this.endIndex = endIndex;
            process.SetSelfToEnd(locations[endIndex]);
        }
        
        public void SetDestinations(Location[] input)
        {
            locations = new Location[input.Length];
            
            for (int i = 0; i < input.Length; i++)
                locations[i] = input[i];
        }

        public float percent { get => process.percent ; set => process.percent = value; }
        
        public void SetDestination(Location value) { process.SetEnd(value); }
        
        public void SetAtRandomPoint() { SetAtPoint(0, Random.Range(0, locations.Length), 1f); }
        public void SetAtPoint(int startIndex, int endIndex, float percent)
        {
            SetEndpoints(startIndex, endIndex);
            this.percent = percent;
        }
        
        public void OnValidate() 
        {
            SetEndpoints(startIndex, endIndex); 
            process.OnValidate(); 
        }
    }
}