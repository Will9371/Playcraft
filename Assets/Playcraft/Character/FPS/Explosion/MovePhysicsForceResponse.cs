using UnityEngine;

namespace Playcraft
{
    public class MovePhysicsForceResponse : MonoBehaviour, IAddForce
    {
        [SerializeField] MovePhysics physics = default;
        [SerializeField] SO slideEffectId;
        [SerializeField] float slideDuration = 0.5f;
        
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force)
        {
            physics.SetSlideForTime(slideEffectId, slideDuration);
        }
    }
}
