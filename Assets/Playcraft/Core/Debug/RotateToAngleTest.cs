using UnityEngine;

namespace Playcraft.Testing
{
    public class RotateToAngleTest : MonoBehaviour
    {
        [SerializeField] float angle;
        [SerializeField] float desiredAngle; 
        [SerializeField] float turnSpeed = 30f;
        [SerializeField] float timeStep = .01f;  
        [SerializeField] bool tick;

        void OnValidate()
        {
            if (!tick) return;
            angle = VectorMath.RotateToAngle(angle, desiredAngle, turnSpeed * timeStep);
            tick = false;
        }
    }
}
