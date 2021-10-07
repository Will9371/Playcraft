using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class LerpRotationIndexOverTime : MonoBehaviour
    {
        [SerializeField] LerpRotationIndex process;
        public float duration;
        [SerializeField] UnityEvent OnComplete;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        
        Transform self => process.self;
        
        void Start() { process.SetSelfIfNull(transform); }
        
        public void SetDuration(float value) { duration = value; }

        public void CycleDestination(bool forward) { process.CycleDestination(forward); }
        
        public void CycleDestinationAndTurn(bool forward)
        {
            CycleDestination(forward);
            BeginTurn();
        }
        
        public void SetDestination(int newIndex) { process.SetDestination(newIndex); }
        
        public void SetDestinationAndTurn(int newIndex)
        {
            SetDestination(newIndex);
            BeginTurn();
        }
        
        public void TurnToNearest(Transform target)
        {
            var direction = (target.position - self.position).normalized;
            TurnToNearest(direction);
        }
        
        public void TurnToNearest(Vector3 desired)
        {
            process.SetDestinationToNearestVector(desired);
            BeginTurn();
        }
        
        public void BeginTurn() { StartCoroutine(Turn()); }
        
        IEnumerator Turn()
        {
            yield return timer.Run(process, duration);
            OnComplete.Invoke();
        }

        public void SetDestinations(Vector3Array value) { SetDestinations(value.values); }
        public void SetDestinations(Vector3[] values) { process.SetDestinations(values); }
        
        public Vector3[] rotations
        {
            get => process.rotations;
            set => process.rotations = value;
        }
    }
}
