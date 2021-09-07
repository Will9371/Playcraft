using System.Collections;
using UnityEngine;

namespace Playcraft
{
    public class LerpPositionOverTime : MonoBehaviour
    {
        [SerializeField] BoolEvent OnComplete;
        [SerializeField] LerpPosition movement;
        [SerializeField] float duration;
        [Tooltip("Sets start location to transform location on application start")]
        [SerializeField] bool startAtSelf;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        
        void Start() 
        { 
            movement.SetSelfIfNull(transform); 
            if (startAtSelf) movement.SetStartAtSelf();
        }

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
        
        public void BeginMove() { StartCoroutine(MoveRoutine()); }
        
        IEnumerator MoveRoutine()
        {                
            timer.SetDurationAndBegin(duration);
            (float percent, bool complete) progress = timer.GetProgress();
            
            while (!progress.complete)
            {
                movement.Input(progress.percent);
                yield return null;
                progress = timer.GetProgress();
            }
            
            OnComplete.Invoke(!movement.reverse);   
        }    
    }
}
