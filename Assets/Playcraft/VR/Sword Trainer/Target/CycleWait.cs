using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Playcraft
{
    public class CycleWait : MonoBehaviour
    {
        [SerializeField] Wait startWait;
        [SerializeField] Wait[] waits;
        
        void Start()
        {
            StartCoroutine(AdvanceRoutine());
        }
        
        IEnumerator AdvanceRoutine()
        {
            yield return WaitAction(startWait);
        
            while(true)
                foreach (var wait in waits)
                    yield return WaitAction(wait);
        }
        
        IEnumerator WaitAction(Wait data)
        {
            var duration = data.RandomDuration;
            yield return new WaitForSeconds(duration * data.actionTime);
            data.action.Invoke();
            yield return new WaitForSeconds(duration * (1 - data.actionTime));
        }
        
        [Serializable] public struct Wait
        {
            public Vector2 range;
            public UnityEvent action;
            [Range(0, 1)] public float actionTime;
            
            public float RandomDuration => Random.Range(range.x, range.y);
        }
    }
}
