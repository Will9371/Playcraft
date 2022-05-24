using System;
using System.Collections;
using UnityEngine;

namespace ZMD
{
    public class LerpPositionOverTimeMono : MonoBehaviour
    {
        [SerializeField] LerpPositionOverTime process;
        
        void Start() { process.Start(); }
        
        public void Move(Vector3 destination, float duration) 
        { StartCoroutine(process.Move(destination, duration)); }

        public void BeginMove() 
        {
            if (!gameObject.activeInHierarchy) return; 
            StartCoroutine(process.Move()); 
        }

        public void MoveForwardByDistance(float distance) 
        {
            if (!gameObject.activeInHierarchy) return; 
            StartCoroutine(process.MoveForwardByDistance(distance)); 
        }
    }
    
    [Serializable]
    public class LerpPositionOverTime : ILerpOverTime
    {
        public LerpPosition movement;
        public float duration;
        [Tooltip("Sets start location to transform location on application start")]
        public bool startAtSelf;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        
        public LerpLocationData data { get => movement.data; set => movement.data = value; }
        
        public void Start() 
        { 
            if (startAtSelf) SetStartAtSelf();
        }
        
        public void SetStartAtSelf() { movement.Initialize(); }
        
        public IEnumerator Move(Vector3 _destination, float _duration)
        {
            SetDuration(_duration);            
            SetDestination(_destination);
            yield return Move();
        }

        public void SetDuration(float value) { duration = value; }
        public void SetDestination(Vector3 value) { movement.SetEnd(value); }
        public void SetPercent(float value) { movement.percent = value; }
        
        public bool useCurve { get => movement.useCurve; set => movement.useCurve = value; }
        public AnimationCurve curve { get => movement.curve; set => movement.curve = value; }
        
        public void FlipPath() { movement.FlipPath(); }
        public void FlipAndRun(MonoBehaviour mono) { FlipPath(); mono.StartCoroutine(Move()); }

        public IEnumerator Move() { yield return timer.Run(movement, duration); }
        
        Transform self => movement.self;
        Vector3 forward => self.forward;
        Vector3 position => movement.useLocal ? self.localPosition : self.position;
        
        public IEnumerator MoveForwardByDistance(float distance)
        {
            var destination = position + forward * distance;
            SetDestination(destination);
            yield return Move();
        }
    }
}
