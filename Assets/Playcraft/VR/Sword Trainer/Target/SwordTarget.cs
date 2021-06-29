using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwordTarget : MonoBehaviour
    {
        [SerializeField] float closeRange;
        [SerializeField] float farRange;
    
        [SerializeField] LerpPositionOverTime movement;
        [SerializeField] Relay activateRelay;
        [SerializeField] GetPercentOverTimeMono activateEffects;
        [SerializeField] Relay deactivateRelay;
        [SerializeField] GetPercentOverTimeMono deactivateEffects;
        [SerializeField] Collider hitbox;
        [SerializeField] CreateFloatingNumberOnHit floatingNumberCreator;
        [SerializeField] BoolEvent OnSetActive;
        [SerializeField] TransformEvent RelayPlayer;
        [SerializeField] MaintainDistance maintainDistance;
        [SerializeField] CircleTarget circleTarget;

        TargetController controller;
        public bool isAlive;
        public bool regenerateFlag;
        
        public void Initialize(TargetController controller, Transform player, Transform canvas)
        {
            this.controller = controller; 
                       
            isAlive = true;
            RelayPlayer.Invoke(player);
            circleTarget.SetTarget(player);
            maintainDistance.SetTarget(player);
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
        
        public void LerpMove(Vector3 destination, float duration)
        {
            if (!isAlive) return;
            movement.Move(destination, duration);
        }
        
        public void MoveComplete()
        {
            if (!isAlive) return;
            Activate();
        }
        
        public void MoveRandom() { controller.MoveSingleTarget(this); }
        
        public void SetRange(bool isClose)
        {
            circleTarget.enabled = !isClose;
            var desiredDistance = isClose ? closeRange : farRange;
            maintainDistance.SetDesiredDistance(desiredDistance);
        }
    }
}
