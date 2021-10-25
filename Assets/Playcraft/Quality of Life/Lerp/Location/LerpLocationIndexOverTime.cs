using System.Collections;
using UnityEngine;

namespace Playcraft
{
    public class LerpLocationIndexOverTime : MonoBehaviour
    {
        [SerializeField] LerpLocationIndex movement;
        [SerializeField] GetPercentOverTime timer = new GetPercentOverTime();
        [SerializeField] IntEvent reachDestinationIndex;
        
        int index;
        
        public void SetDuration(float value) { timer.duration = value; }
        
        public void BeginMove(int destinationIndex) 
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(Move(destinationIndex, false));
        }
        
        public void BeginMoveFromSelf(int destinationIndex)
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(Move(destinationIndex, true));    
        }
        
        IEnumerator Move(int destinationIndex, bool moveFromSelf) 
        {
            index = destinationIndex;
            
            // * Refactor if this gets more complex
            if (moveFromSelf)
                movement.SetSelfToEnd(destinationIndex);
            else
                movement.SetDestination(destinationIndex);
         
            yield return timer.Run(movement);

            reachDestinationIndex.Invoke(index);
        }
        
        void OnValidate() { movement.Validate(); }
    }
}
