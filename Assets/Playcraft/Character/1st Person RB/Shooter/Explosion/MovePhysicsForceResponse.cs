using UnityEngine;

namespace Playcraft
{
    public class MovePhysicsForceResponse : MonoBehaviour, IAddForce
    {
        [SerializeField] MovePhysics physics;
        [SerializeField] float slideDuration;
        
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force)
        {
            physics.SetSlideForTime(slideDuration);
        }
    }
}
