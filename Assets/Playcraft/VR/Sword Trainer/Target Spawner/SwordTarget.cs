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
        [SerializeField] CreateFloatingNumberOnHit floatingNumberCreator;
        [SerializeField] BoolEvent OnSetActive;
        [SerializeField] TransformEvent RelayPlayer;

        TargetController controller;
        public bool isAlive;
        public bool regenerateFlag;
        
        public void Initialize(TargetController controller, Transform player, Transform canvas)
        {
            this.controller = controller; 
                       
            isAlive = true;
            RelayPlayer.Invoke(player);
            floatingNumberCreator.SetFloaterCanvas(canvas);
        }
                
        public void Activate()
        {
            if (!regenerateFlag) return;
            
            activateRelay.Input();
            activateEffects.Begin();
            regenerateFlag = false;
        }
        
        public void Deactivate()
        {
            isAlive = false;
            hitbox.enabled = false;
            deactivateRelay.Input();
            deactivateEffects.Begin();
            OnSetActive.Invoke(false);
        }
        
        public void ActivationComplete() 
        { 
            OnSetActive.Invoke(true); 
            hitbox.enabled = true;
        }
        
        public void DeactivationComplete() { controller.TargetHit(); }
        
        public void Move(Vector3 destination, float duration)
        {
            if (!isAlive) return;
            
            movement.SetDestination(destination);
            moveTimer.SetDuration(duration);
            moveTimer.Begin();
        }
        
        public void MoveComplete()
        {
            if (!isAlive) return;
            Activate();
        }
        
        public void MoveRandom() { controller.MoveSingleTarget(this); }
    }
}
