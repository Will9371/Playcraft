using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Allows non-monobehaviour classes to use monobehaviour methods
namespace Playcraft
{
    public class MonoSim : Singleton<MonoSim>
    {
        public void SimInvoke(Action invoked, float time)
        {
            MonoBehaviourExtensions.Invoke(this, invoked, time);
        }

        public void SimInvoke(Action<GameObject> invoked, GameObject obj, float time)
        {
            MonoBehaviourExtensions.Invoke(this, invoked, obj, time);
        }

        public void SimInvokeRepeating(Action invoked, float startTime, float repeatTime)
        {
            MonoBehaviourExtensions.InvokeRepeating(this, invoked, startTime, repeatTime); 
        }

        public void SimCancelInvoke(Action invoked)
        {
            MonoBehaviourExtensions.CancelInvoke(this, invoked);
        }

        public void SimCancelInvoke(Action<GameObject> invoked)
        {
            MonoBehaviourExtensions.CancelInvoke(this, invoked);
        }
    }

    // Can be called directly (from a MonoBehaviour derived class), or indirectly through MonoSim
    public static class MonoBehaviourExtensions
    {
        static List<RepeatInvokeInfo> _runningLoops = new List<RepeatInvokeInfo>();
        static List<RepeatInvokeInfo_GameObject> _runningLoops_GameObject = new List<RepeatInvokeInfo_GameObject>();

        // Allows use of Invoke with a delegate instead of a string
        public static void Invoke(this MonoBehaviour mono, Action _delegate, float time)
        {
            mono.StartCoroutine(ExecuteAfterTime(_delegate, time));
        }

        public static IEnumerator ExecuteAfterTime(Action _delegate, float delay)
        {
            yield return new WaitForSeconds(delay);
            _delegate();
        }

        // Invoke with a delegate, pass back a GameObject
        public static void Invoke(this MonoBehaviour mono, Action<GameObject> _delegate, GameObject obj, float time)
        {
            mono.StartCoroutine(ExecuteAfterTime(_delegate, obj, time));
        }

        public static IEnumerator ExecuteAfterTime(Action<GameObject> _delegate, GameObject obj, float delay)
        {
            yield return new WaitForSeconds(delay);
            _delegate(obj);
        }

        // Allows use of InvokeRepeating with a delegate instead of a string
        public static void InvokeRepeating(this MonoBehaviour mono, Action _delegate, float startTime, float repeatTime)
        {
            RepeatInvokeInfo newLoop = new RepeatInvokeInfo();

            newLoop.loop = mono.StartCoroutine(ExecuteAfterTimeRepeating(_delegate, startTime, repeatTime));
            newLoop._delegate = _delegate;

            _runningLoops.Add(newLoop);
        }

        public static IEnumerator ExecuteAfterTimeRepeating(Action _delegate, float startDelay, float repeatDelay)
        {
            yield return new WaitForSeconds(startDelay);

            while (true)
            {
                _delegate();
                yield return new WaitForSeconds(repeatDelay);
            }
        }

        // Cancels a specific Invoke
        public static void CancelInvoke(this MonoBehaviour mono, Action _delegate)
        {
            foreach (RepeatInvokeInfo loop in _runningLoops)
                if (loop._delegate == _delegate)
                    mono.StopCoroutine(loop.loop);
        }

        public static void CancelInvoke(this MonoBehaviour mono, Action<GameObject> _delegate)
        {
            foreach (RepeatInvokeInfo_GameObject loop in _runningLoops_GameObject)
                if (loop._delegate == _delegate)
                    mono.StopCoroutine(loop.loop);
        }
    }

    // Stores identification for a specific InvokeRepeating
    public struct RepeatInvokeInfo
    {
        public Coroutine loop;
        public Action _delegate;
    }

    public struct RepeatInvokeInfo_GameObject
    {
        public Coroutine loop;
        public Action<GameObject> _delegate;
    }
}

