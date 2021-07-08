using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class LerpPositionOverTime : MonoBehaviour
    {
        [SerializeField] UnityEvent OnComplete;
        [SerializeField] LerpPosition movement;
        
        void Start() { movement.SetSelfIfNull(transform); }

        public void Move(Vector3 destination, float duration)
        {            
            movement.SetEnd(destination);
            StartCoroutine(MoveRoutine(duration));
        }
        
        GetPercentOverTime timer = new GetPercentOverTime();
        
        IEnumerator MoveRoutine(float duration)
        {                
            timer.SetDurationAndBegin(duration);
            (float percent, bool complete) progress = timer.GetProgress();
            
            while (!progress.complete)
            {
                movement.Input(progress.percent);
                yield return null;
                progress = timer.GetProgress();
            }
            
            OnComplete.Invoke();   
        }    
    }
}
