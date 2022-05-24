using System;
using System.Collections;
using UnityEngine;

namespace ZMD
{
    public class LerpLocationIndexOverTimeMono : MonoBehaviour
    {
        [SerializeField] LerpLocationIndexOverTime process;
        [SerializeField] IntEvent reachDestinationIndex;

        public void SetDuration(float value) { process.duration = value; }

        /// Move from current location to indexed location
        public void BeginMove(int destinationIndex)
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(Move(destinationIndex));    
        }
        
        IEnumerator Move(int destinationIndex)
        {
            yield return process.Move(destinationIndex);
            reachDestinationIndex.Invoke(destinationIndex);
        }
        
        public void SetDestinations(Transform[] values) { process.SetDestinations(values); }

        void OnValidate() { process.OnValidate(); }
    }
    
    [Serializable]
    public class LerpLocationIndexOverTime
    {
        public LerpLocationIndex movement;
        public GetPercentOverTime timer;

        public int index { get; private set; }

        public IEnumerator Move(int destinationIndex) 
        {
            index = destinationIndex;
            movement.SetSelfToEnd(destinationIndex);
            yield return timer.Run(movement);
        }
        
        public void SetAtRandomPoint() { movement.SetAtRandomPoint(); }
        public void SetAtPoint(int startIndex, int endIndex, float percent) { movement.SetAtPoint(startIndex, endIndex, percent); }
        
        public void Interrupt() { timer.interruptFlag = true; }

        public void OnValidate() { movement.OnValidate(); }
        
        #region Set Properties
        
        public float duration { set => timer.duration = value; }
        
        public LerpLocationData data { set => movement.data = value; }

        public void SetDestinations(Transform[] values) 
        {
            var locations = new Location[values.Length];
            for (int i = 0; i < values.Length; i++)
                locations[i] = new Location(values[i]);
         
            movement.SetDestinations(locations); 
        }
        
        public void SetDestinations(Vector3[] positions, Quaternion[] rotations)
        {
            var length = positions.Length;
            var locations = new Location[length];
            
            for (int i = 0; i < length; i++)
                locations[i] = new Location(positions[i], rotations[i]);
         
            movement.SetDestinations(locations);             
        }
        
        public void SetDestinations(Vector3[] positions, Quaternion[] rotations, Vector3[] scales)
        {
            var length = positions.Length;
            var locations = new Location[length];
            
            for (int i = 0; i < length; i++)
                locations[i] = new Location(positions[i], rotations[i], scales[i]);
         
            movement.SetDestinations(locations);             
        }
        
        #endregion
    }
}
