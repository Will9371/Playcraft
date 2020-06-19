using UnityEngine;

namespace Playcraft
{
    public class MoveTowardsDrift : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float acceleration = 1f;
        [SerializeField] float turnSpeed = 1f;
        [SerializeField] bool multiplyTurnSpeedByMoveSpeed;
        #pragma warning restore 0649
        
        Vector3 moveDirection;
        float moveSpeed;
        
        public void SetSpeed(float value) { moveSpeed = value; }
        
        public void Input(Vector3 self, Vector3 target, float speed)
        {
            var desiredDirection = (target - self).normalized;
            var adjustedTurnSpeed = multiplyTurnSpeedByMoveSpeed ? turnSpeed * speed : turnSpeed;
            
            moveSpeed = VectorMath.MoveTowards(moveSpeed, speed, acceleration);
            
            if (moveDirection == Vector3.zero) moveDirection = desiredDirection;
            moveDirection = VectorMath.MoveTowards(moveDirection, desiredDirection, adjustedTurnSpeed);
            
            var distance = Vector3.Distance(self, target);
            var moveVector = moveDirection * distance;
            transform.position = Vector3.MoveTowards(self, self + moveVector, moveSpeed * Time.deltaTime);
            
            Debug.DrawLine(self, target, Color.red);
            Debug.DrawLine(self, self + moveVector, Color.green);
        }
    }
}
