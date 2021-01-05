using UnityEngine;

namespace Playcraft.Testing
{
    public class RotateToAngleTest : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float angle;
        [SerializeField] float desiredAngle; 
        [SerializeField] float turnSpeed = 30f;
        [SerializeField] float timeStep = .01f;  
        [SerializeField] bool tick;
        #pragma warning restore 0649

        void OnValidate()
        {
            if (!tick) return;
            angle = VectorMath.RotateToAngle(angle, desiredAngle, turnSpeed * timeStep);
            tick = false;
        }
    }
}
