using System.Collections;
using UnityEngine;

// REFACTOR: split into Mono/Non-Mono
namespace Playcraft
{
    public class LerpPositionOverTime : MonoBehaviour
    {
        [SerializeField] BoolEvent OnComplete;
        [SerializeField] LerpPosition movement;
        public float duration;
        [Tooltip("Sets start location to transform location on application start")]
        [SerializeField] bool startAtSelf;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        
        void Start() 
        { 
            movement.SetSelfIfNull(transform); 
            if (startAtSelf) SetStartAtSelf();
        }
        
        public void SetStartAtSelf() { movement.Initialize(); }
        
        public void Move(Vector3 _destination, float _duration)
        {
            SetDuration(_duration);            
            SetDestination(_destination);
            BeginMove();
        }
        
        public void SwitchDirectionAndMove() 
        {
            movement.SwitchDirection();
            BeginMove();
        }
        
        public void SetDirectionAndMove(bool forward)
        {
            SetDirection(forward);
            BeginMove();
        }
        
        public void MoveIfNewDirection(bool forward)
        {
            var hasChanged = movement.reverse == forward;
            if (!hasChanged) return;
            SetDirection(forward);
            BeginMove();
        }
        
        public void SetDuration(float value) { duration = value; }
        public void SetDestination(Vector3 value) { movement.SetEnd(value); }
        public void SetDirection(bool forward) { movement.reverse = !forward; }
        public void SetPercent(float value) { movement.percent = value; }
        
        public void BeginMove() 
        {
            if (!gameObject.activeInHierarchy) return; 
            StartCoroutine(MoveRoutine()); 
        }
        
        public IEnumerator MoveRoutine()
        {
            yield return timer.Run(movement, duration);
            OnComplete.Invoke(!movement.reverse);
        }
        
        Transform self => movement.self;
        Vector3 forward => self.forward;
        Vector3 position => movement.useLocal ? self.localPosition : self.position;
        
        public void MoveForwardByDistance(float distance)
        {
            var destination = position + forward * distance;
            SetDestination(destination);
            BeginMove();
        }
    }
}
