using System.Collections;
using UnityEngine;

namespace Playcraft
{
    public class LerpTransformIndexOverTimeMono : MonoBehaviour
    {
        [SerializeField] LerpTransformIndexOverTime process;
        [SerializeField] IntEvent reachDestinationIndex;
        
        public float duration => process.timer.duration;
        
        void OnValidate() { process.OnValidate(); }

        public void SetDuration(float value) { process.SetDuration(value); }
        
        public void BeginMove(int destinationIndex)
        {
            if (!gameObject.activeInHierarchy) return;
            StartCoroutine(Move(destinationIndex));    
        }
        
        /// Move from current location to indexed location
        IEnumerator Move(int destinationIndex) 
        {
            yield return process.Move(destinationIndex);
            reachDestinationIndex.Invoke(destinationIndex);
        }
        
        public void SetDestinations(Transform[] values) { process.SetDestinations(values); }
    }
}
