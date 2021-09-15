using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class LerpPositionAndBack : MonoBehaviour
    {
        [SerializeField] LerpPosition[] movers;
        [SerializeField] Stage activeStage;
        [SerializeField] Stage returnStage;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        
        public void Begin() 
        {
            if (!gameObject.activeSelf)
                return;
         
            StartCoroutine(Process(activeStage)); 
        }

        IEnumerator Process(Stage stage)
        {
            timer.SetDurationAndBegin(stage.duration);
                
            while (timer.inProgress)
            {
                yield return null;
                foreach (var mover in movers)
                    mover.Input(timer.percent);
            }
            
            stage.onComplete.Invoke();
        }
        
        public void BeginInterrupt() 
        {
            StopAllCoroutines();
            StartCoroutine(Interrupt()); 
        }
        
        IEnumerator Interrupt()
        {
            foreach (var mover in movers)
            {
                mover.CachePath();
                mover.SetPathToStartFromSelf();
            }
            
            yield return StartCoroutine(Process(returnStage));
            
            foreach (var mover in movers)
                mover.ResetPathFromCache();
        }
        
        [Serializable]
        public class Stage
        {
            public float duration;
            public UnityEvent onComplete;
        }
    }
}