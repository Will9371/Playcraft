using Playcraft;
using UnityEngine;

public class RotateTowardsTest : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] Vector2 desiredDirection;
    [SerializeField] float turnSpeed;
    [SerializeField] float timeStep = .01f;
    [SerializeField] bool tick;

    private void OnValidate()
    {
        if (!tick) return;
        
        direction = VectorMath.RotateTowards(direction, desiredDirection, turnSpeed, timeStep);
        tick = false;
    }
}
