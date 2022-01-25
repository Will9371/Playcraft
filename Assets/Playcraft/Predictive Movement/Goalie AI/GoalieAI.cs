using System;
using UnityEngine;

namespace ZMD.PredictiveMovement
{
    [Serializable]
    public class GoalieAI
    {
        [Header("References")]
        public Transform self;
        public Transform target;
        public Transform center;
        
        [Header("Settings")]
        public float speed = 5f;
        
        public bool stayInCircle;
        public float radius = 2f;
        FollowOnCircle bounds = new FollowOnCircle();

        [SerializeField] DestinationModifiers destinationModifiers;

        public void Initialize() 
        {
            bounds.radius = radius; 
            bounds.center = center.position;
        }
        
        Vector3 targetPosition;

        public void FixedUpdate() 
        {
            targetPosition = destinationModifiers.Tick(target.position);
            if (stayInCircle) targetPosition = bounds.Update(targetPosition);
            self.position = Vector3.MoveTowards(self.position, targetPosition, speed * Time.deltaTime);
        }
    }
}
