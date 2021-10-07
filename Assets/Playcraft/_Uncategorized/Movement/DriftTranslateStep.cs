using System;
using UnityEngine;

// Combination of CombineAsyncVectors -> MoveDirectionDrift -> TranslateStep.
// Functionality of each was too trivial to delegate
namespace Playcraft
{
    [Serializable] public class DriftTranslateStep
    {
        public Transform self;
        public float speed;
        public float acceleration = 1.5f; 
            
        Vector3 inputDirection;
        Vector3 moveDirection;
               
        public void AddMovement(Vector3 value) { inputDirection += value; }
        
        public void Update()
        {
            SetMoveDirection(inputDirection.normalized);
            Step(moveDirection);
            inputDirection = Vector3.zero; 
        }
            
        void SetMoveDirection(Vector3 desiredDirection)
        {
            if (moveDirection == Vector3.zero) moveDirection = desiredDirection;
            moveDirection = VectorMath.MoveTowards(moveDirection, desiredDirection, acceleration);
        }
        
        Vector3 moveStep;
            
        void Step(Vector3 direction) 
        {
            moveStep = speed * Time.deltaTime * direction;
            self.Translate(moveStep); 
        }
    }
}
