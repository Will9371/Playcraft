using System.Collections;
using UnityEngine;

// REFACTOR: repeat logic from LerpPositionOverTime
namespace Playcraft
{
    public class LerpPositionIndexOverTime : MonoBehaviour
    {
        [SerializeField] LerpPositionIndex movement;
        [SerializeField] GetPercentOverTime timer = new GetPercentOverTime();
        
        public void SetDuration(float value) { timer.duration = value; }
        
        public void BeginMove(int destinationIndex) 
        {
            if (!gameObject.activeInHierarchy) return; 
            movement.SetDestination(destinationIndex);
            StartCoroutine(Move());
        }
        
        IEnumerator Move() { yield return timer.Run(movement); }
        
        IEnumerator MoveAtSpeed(float speed)
        {
            if (speed <= 0f) 
            {
                Debug.LogError($"Invalid move speed {speed}");
                yield break;
            }
            
            timer.duration = movement.distance/speed;
            yield return timer.Run(movement);
        }
    }
}
