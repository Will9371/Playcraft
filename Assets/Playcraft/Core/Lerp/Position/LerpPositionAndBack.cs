using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

// * Not fully tested/reliable
namespace Playcraft
{
    public class LerpPositionAndBack : MonoBehaviour
    {
        [SerializeField] LerpPosition[] movers;
        [SerializeField] Stage activeStage;
        [SerializeField] Stage returnStage;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        IPercent[] processes;
        
        void Awake()
        {
            processes = new IPercent[movers.Length];
            for (int i = 0; i < movers.Length; i++)
                processes[i] = movers[i];
        }
        
        public void Begin() 
        {
            if (!gameObject.activeSelf)
                return;
         
            StartCoroutine(Process(activeStage)); 
        }

        IEnumerator Process(Stage stage)
        {
            yield return timer.Run(processes, stage.duration);
            stage.onComplete.Invoke();
        }
        
        public void BeginInterrupt() 
        {
            //Debug.Log($"LerpPositionAndBack.BeginInterrupt() at time {Time.time}");
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
        
        void OnDisable()
        {
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