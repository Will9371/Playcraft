using Playcraft;
using UnityEngine;

public class MoveDirectionDrift : MonoBehaviour
{
    [SerializeField] float turnSpeed = 1f;
    [SerializeField] Vector3Event Output;
    
    Vector3 moveDirection;

    public void Input(Vector3 desiredDirection)
    {
        if (moveDirection == Vector3.zero) moveDirection = desiredDirection;
        moveDirection = VectorMath.MoveTowards(moveDirection, desiredDirection, turnSpeed);
        Output.Invoke(moveDirection);
    }
}
