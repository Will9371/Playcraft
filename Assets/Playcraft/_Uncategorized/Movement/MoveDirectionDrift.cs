using UnityEngine;

namespace Playcraft
{
    public class MoveDirectionDrift : MonoBehaviour
    {
        [SerializeField] float acceleration = 1.5f;
        [SerializeField] Vector3Event Output;
        
        Vector3 moveDirection;

        public void Input(Vector3 desiredDirection)
        {
            if (moveDirection == Vector3.zero) moveDirection = desiredDirection;
            moveDirection = VectorMath.MoveTowards(moveDirection, desiredDirection, acceleration);
            Output.Invoke(moveDirection);
        }
    }
}
