using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class SwordTarget : MonoBehaviour
    {
        [SerializeField] float closeRange;
        [SerializeField] float farRange;
    
        [SerializeField] LerpPositionOverTime movement;
        [SerializeField] GetPercentOverTimeMono activateEffects;
        [SerializeField] LerpColorMono colorChange;
        [SerializeField] LerpScaleMono scaleChange;
        [SerializeField] GetPercentOverTimeMono deactivateEffects;
        [SerializeField] Collider hitbox;
        [SerializeField] CreateFloatingNumberOnHit floatingNumberCreator;
        [SerializeField] BoolEvent OnSetActive;
        [SerializeField] TransformEvent RelayPlayer;
        [SerializeField] CircleTargetAtDistance movementAI;

        TargetController controller;
        public bool isAlive;
        public bool regenerateFlag;
        
        public void Initialize(TargetController controller, Transform player, Transform canvas)
        {
            this.controller = controller; 
                       
            isAlive = true;
            RelayPlayer.Invoke(player);
            movementAI.RandomizeWeights();
            movementAI.SetTarget(player);
            floatingNumberCreator.SetFloaterCanvas(canvas);
        }
                
        public void Activate()
        {
            if (!regenerateFlag) return;
            
            colorChange.SetDirection(false);
            scaleChange.SetDirection(false);
            
            activateEffects.Begin();
            regenerateFlag = false;
        }
        
        public void Deactivate()
        {
            isAlive = false;
            hitbox.enabled = false;
            
            colorChange.SetDirection(true);
            scaleChange.SetDirection(true);
            
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
            movementAI.circlingEnabled = !isClose;
            var desiredDistance = isClose ? closeRange : farRange;
            movementAI.SetDesiredDistance(desiredDistance);
        }
    }
}
