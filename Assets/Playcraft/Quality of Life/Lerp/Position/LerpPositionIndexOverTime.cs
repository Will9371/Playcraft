using System.Collections;
using UnityEngine;

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
    }
}