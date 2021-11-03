using System.Collections;
using UnityEngine;

namespace Playcraft
{
    public class LerpLocationIndexOverTime : MonoBehaviour
    {
        [SerializeField] LerpLocationIndex movement;
        [SerializeField] GetPercentOverTime timer = new GetPercentOverTime();
        [SerializeField] IntEvent reachDestinationIndex;
        
        public float duration => timer.duration;
        
        int index;
        
        public void SetDuration(float value) { timer.duration = value; }

        /// Move from current location to indexed location
        public void BeginMove(int destinationIndex)
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(Move(destinationIndex));    
        }
        
        IEnumerator Move(int destinationIndex) 
        {
            index = destinationIndex;
            movement.SetSelfToEnd(destinationIndex);
            
            yield return timer.Run(movement);

            reachDestinationIndex.Invoke(index);
        }
        
        public void SetDestinations(Transform[] values) { movement.SetDestinations(values); }
        
        void OnValidate() { movement.Validate(); }
    }
}
