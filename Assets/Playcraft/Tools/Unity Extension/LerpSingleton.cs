using System;
using System.Collections;
using UnityEngine;

namespace Playcraft
{
    public class LerpSingleton : Singleton<LerpSingleton>
    {
        public void BeginMove(Transform self, Vector3 to, float time, Action OnEnd = null)
        {
            StartCoroutine(MoveRoutine(self, to, time, OnEnd));
        }
    
        IEnumerator MoveRoutine(Transform self, Vector3 to, float time, Action OnEnd = null)
        {
            var from = self.position;
            var startTime = Time.time;
            var percent = 0f;
                
            while (percent < 1f)
            {
                percent = (Time.time - startTime)/time;
                self.position = Vector3.Lerp(from, to, percent);
                yield return null;
            }
            
            if (OnEnd != null) OnEnd();
        }
    }    
}
