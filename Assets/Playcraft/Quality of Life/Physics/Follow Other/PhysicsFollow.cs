using UnityEngine;
using Playcraft;

public class PhysicsFollow : MonoBehaviour
{
    [SerializeField] Rigidbody self;
    [SerializeField] Transform target;
    [SerializeField] PositionPID positionPID;
    [SerializeField] TorqueLookToward torqueLookToward;
    
    void OnValidate()
    {
        positionPID.rb = self;
        torqueLookToward.rb = self;
        torqueLookToward.target = target;
    }

    void FixedUpdate()
    {
        torqueLookToward.FixedUpdate();
        positionPID.targetPosition = target.position;
        positionPID.FixedUpdate();
    }
}
