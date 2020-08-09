using UnityEngine;

namespace Playcraft.Testing
{
    public class RotateTowardsTest : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector2 direction;
        [SerializeField] Vector2 desiredDirection;
        [SerializeField] float turnSpeed;
        [SerializeField] float timeStep = .01f;
        [SerializeField] bool tick;
        #pragma warning restore 0649

        private void OnValidate()
        {
            if (!tick) return;
            
            direction = VectorMath.RotateTowards(direction, desiredDirection, turnSpeed, timeStep);
            tick = false;
        }
    }
}
