using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playcraft
{
    [Serializable]
    public class LerpLocationIndex : IPercent
    {
        [SerializeField] LerpLocation process;
        public Location[] locations;

        public int startIndex; 
        public int endIndex;
        
        public void SetDestination(int newIndex) { SetEndpoints(endIndex, newIndex); }
        
        public void SetRandomDestination() { SetDestination(GetRandomDestination()); }
        public int GetRandomDestination() { return Random.Range(0, locations.Length); }
        
        public void SetEndpoints(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
            process.SetStart(locations[startIndex]);
            process.SetEnd(locations[endIndex]);
        }
        
        public void SetSelfToEnd(int endIndex)
        {
            if (endIndex >= locations.Length || endIndex < 0)
            {
                Debug.LogError($"{self.name}: index {endIndex} out of range, max is {locations.Length - 1}", self.gameObject);
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

        public Transform self { get => process.self; set => process.self = value; }
        public float percent { get => process.percent ; set => process.percent = value; }
        public void SetDestination(Location value) { process.SetEnd(value); }
        
        public void OnValidate() 
        {
            SetEndpoints(startIndex, endIndex); 
            process.OnValidate(); 
        }
        
        public void SetUseLocal(bool value) { process.useLocal = value; }
    }
}