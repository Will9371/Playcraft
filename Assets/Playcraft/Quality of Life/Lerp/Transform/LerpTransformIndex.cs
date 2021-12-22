using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Playcraft
{
    [Serializable]
    public class LerpTransformIndex : IPercent
    {
        [SerializeField] LerpTransform process;
        public Transform[] locations;

        public int startIndex; 
        public int endIndex;
        
        public void SetDestination(int newIndex) { SetEndpoints(endIndex, newIndex); }
        
        public void SetRandomDestination() { SetDestination(GetRandomDestination()); }
        public int GetRandomDestination() { return Random.Range(0, locations.Length); }
        
        void SetEndpoints(int startIndex, int endIndex)
        {
            this.startIndex = startIndex;
            this.endIndex = endIndex;
        }
        
        public void SetSelfToEnd(int endIndex)
        {
            this.endIndex = endIndex;
            process.SetSelfToEnd(locations[endIndex]);
            //Debug.Log($"{process.self} set to move from {startIndex} to {endIndex}"); 
        }
        
        public void SetDestinations(Transform[] input)
        {
            if (input == null) return;
            
            locations = new Transform[input.Length];
            
            for (int i = 0; i < input.Length; i++)
                locations[i] = input[i];
        }

        public Transform self { get => process.self; set => process.self = value; }
        public Transform start { get => process.start; set => process.start = value; }
        public Transform end { get => process.end; set => process.end = value; }
        public float percent { get => process.percent; set => process.percent = value; }
        
        public void SetDestination(Transform value) { process.SetEnd(value); }
        public void SetUseLocal(bool value) { process.useLocal = value; }
        public void SetCurve(bool useCurve, AnimationCurve curve) { process.SetCurve(useCurve, curve); }
        
        public void OnValidate() { process.OnValidate(); }
    }
}