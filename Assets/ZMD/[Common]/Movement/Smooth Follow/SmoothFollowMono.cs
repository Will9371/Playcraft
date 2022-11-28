using System;
using System.Collections;
using UnityEngine;

namespace ZMD
{
    /// Access SmoothFollow as a standalone component
    /// Coordinates continuous movement and rotation to follow a target transform
    public class SmoothFollowMono : MonoBehaviour
    {
        [SerializeField] SmoothFollow process;
        
        void Update() { process.Update(); }
        public void SetTarget(Transform value) { process.SetTarget(value); }
        
        void OnValidate() 
        {
            process.SetSelf(transform); 
            process.OnValidate(); 
        }
    }
    
    /// Coordinates continuous movement and rotation to follow a target transform
    [Serializable]
    public class SmoothFollow
    {
        public Transform target;
        public float moveSpeed;
        public float turnSpeed;
        
        [HideInInspector] public SmoothFollowPosition position;
        [HideInInspector] public SmoothFollowRotation rotation;
        
        public void Update()
        {
            position.Update();
            rotation.Update();
        }
        
        public void OnValidate()
        {
            SetTarget(target);
            position.speed = moveSpeed;
            rotation.speed = turnSpeed;
        }
        
        public void SetSelf(Transform value)
        {
            position.self = value;
            rotation.self = value;
        }
        
        public void SetTarget(Transform value)
        {
            target = value;
            position.target = value;
            rotation.target = value;
        }
        
        public void SetSpeed(float moveSpeed, float turnSpeed)
        {
            this.moveSpeed = moveSpeed;
            this.turnSpeed = turnSpeed;
            position.speed = moveSpeed;
            rotation.speed = turnSpeed;
        }
        
        /// Use this in exclusion with Update
        /// or else object will move at double speed
        public IEnumerator MoveUntilAtTarget(float stoppingDistance = .01f, float stoppingAngle = 1f)
        {
            while (true)
            {
                position.Update();
                rotation.Update();
                yield return null;
                
                if (position.targetDistance <= stoppingDistance &&
                    rotation.angleToTarget <= stoppingAngle)
                    break;
            }
        }
    }
}