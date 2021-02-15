using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwordTarget : MonoBehaviour
    {
        [SerializeField] LerpPosition movement;
        [SerializeField] GetPercentOverTime moveTimer;
        [SerializeField] Relay activateRelay;
        [SerializeField] GetPercentOverTime activateEffects;
        [SerializeField] Relay deactivateRelay;
        [SerializeField] GetPercentOverTime deactivateEffects;
        [SerializeField] Collider hitbox;

        TargetController controller;
        
        public void Initialize(TargetController controller)
        {
            this.controller = controller;
        }
        
        public void Activate()
        {
            activateRelay.Input();
            activateEffects.Begin();
        }
        
        public void Deactivate()
        {
            hitbox.enabled = false;
            deactivateRelay.Input();
            deactivateEffects.Begin();
            controller.TargetHit();
        }
        
        public void Move(Vector3 destination, float duration)
        {
            movement.SetDestination(destination);
            moveTimer.SetDuration(duration);
            moveTimer.Begin();
        }
    }
}
