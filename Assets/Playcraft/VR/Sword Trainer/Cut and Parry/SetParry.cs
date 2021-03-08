using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    public class SetParry : MonoBehaviour
    {
        [SerializeField] ParryTargetOrbState[] orbs;
        [SerializeField] UnityEvent OnParryComplete;
        
        #region Reference injection
        
        int uniqueParryCount;
        float transitionTime;
        LerpPosition movement;
        LerpRotation rotation;
        GetPercentOverTime timer;
        
        public void Inject(float transitionTime, LerpPosition movement, 
        LerpRotation rotation, GetPercentOverTime timer)
        {
            this.transitionTime = transitionTime;
            this.movement = movement;
            this.rotation = rotation;
            this.timer = timer;
            
            uniqueParryCount = movement.positions.Length;
            
            if (rotation._rotations.Length != movement.positions.Length)
                Debug.LogError("Non-matching number of parry positions and rotations");
        }
        
        #endregion

        public void SetRandomParry()
        {        
            ActivateOrbs(false);
            var nextParryIndex = Random.Range(0, uniqueParryCount);
            
            rotation.SetDestination(nextParryIndex);
            movement.SetDestination(nextParryIndex);
            timer.Begin();
            
            Invoke(nameof(ActivateOrbsDelayTrue), transitionTime);
        }
        
        public void ParryComplete(bool value = true)
        {
            if(!value) return;
            ActivateOrbs(false);
            OnParryComplete.Invoke();
        }
        
        void ActivateOrbsDelayTrue() { ActivateOrbs(true); }
        
        public void ActivateOrbs(bool value)
        {
            foreach (var orb in orbs)
                orb.SetReadyToParry(value);
        }
    }
}
