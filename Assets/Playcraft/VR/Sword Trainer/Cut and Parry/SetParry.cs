using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    public class SetParry : MonoBehaviour
    {
        [SerializeField] ParryTargetOrbState[] orbs;
        [SerializeField] UnityEvent OnParryComplete;
        [SerializeField] UnityEvent OnParryReady;
        
        #region Reference injection
        
        int uniqueParryCount;
        float transitionTime;
        LerpPositionIndexMono movement;
        LerpRotationIndex rotation;
        GetPercentOverTimeMono timer;
        
        public void Inject(float transitionTime, LerpPositionIndexMono movement, 
        LerpRotationIndex rotation, GetPercentOverTimeMono timer)
        {
            this.transitionTime = transitionTime;
            this.movement = movement;
            this.rotation = rotation;
            this.timer = timer;
            
            uniqueParryCount = movement.positions.Length;
            
            if (rotation.rotations.Length != movement.positions.Length)
                Debug.LogError("Non-matching number of parry positions and rotations");
        }
        
        #endregion

        public void SetRandomParry(bool readyOnSet)
        {
            ActivateOrbs(false);
            var nextParryIndex = Random.Range(0, uniqueParryCount);
            
            rotation.SetDestination(nextParryIndex);
            movement.SetDestination(nextParryIndex);
            timer.Begin();
            
            if (readyOnSet)
                SetParryReady();
        }
        
        public void SetParryReady()
        {
            OnParryReady.Invoke();
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
