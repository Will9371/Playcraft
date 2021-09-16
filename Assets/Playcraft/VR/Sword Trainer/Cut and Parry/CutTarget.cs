using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutTarget : MonoBehaviour
    {
        [SerializeField] float retractDelay;
        [SerializeField] bool activateOnStart;
        [SerializeField] BoolEvent OnCutComplete;
        
        [SerializeField] Collider[] colliders;
        [SerializeField] GetPercentOverTimeMono extend;
        [SerializeField] GetPercentOverTimeMono retract;
        [SerializeField] DisplaySequenceByColor hitStatus;

        int activeIndex;
        
        void Start()
        {
            if (!activateOnStart) return;
            Hit(0);
            extend.Begin();
        }

        public void SetRandomCut() { extend.Begin(); }
        
        public void Hit(int index) 
        {
            //Debug.Log($"Hit: index = {index}, active index = {activeIndex}");
            if (index != activeIndex) return;
            
            activeIndex++;
            hitStatus.Input(activeIndex);

            if (activeIndex > colliders.Length)
                Success(retractDelay);
        }
        
        public void Success(float delayToRetract) { CutComplete(delayToRetract, true); }
        public void Fail(float delayToRetract) { CutComplete(delayToRetract, false); }
        
        public void CutComplete(float delayToRetract, bool success)
        {
            SetCollidersEnabled(false);
            Invoke(nameof(Retract), delayToRetract);
            OnCutComplete.Invoke(success);
        }
        
        void Retract()
        {
            activeIndex = 0;
            retract.Begin();
            hitStatus.Input(activeIndex);           
        }
        
        bool collidersEnabled;
        
        public void SetCollidersEnabled(bool value)
        {
            collidersEnabled = value;
        
            foreach (var col in colliders)
                col.enabled = value;
        }
        
        public void ActiveReset() { ActiveReset(true); }
        public void ActiveReset(bool forward)
        {
            if (!forward || !collidersEnabled)
                return;
                
            activeIndex = 1;
            hitStatus.Input(activeIndex);
        }
    }
}
