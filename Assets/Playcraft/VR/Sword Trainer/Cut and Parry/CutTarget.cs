using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutTarget : MonoBehaviour
    {
        [SerializeField] float retractDelay;
        [SerializeField] bool activateOnStart;
        [SerializeField] UnityEvent OnCutComplete;
        
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

        public void SetRandomCut()
        {
            extend.Begin();
        }
        
        public void Hit(int index) 
        {
            //Debug.Log($"Hit: index = {index}, active index = {activeIndex}");
            if (index != activeIndex) return;
            
            activeIndex++;
            hitStatus.Input(activeIndex);

            if (activeIndex > colliders.Length)
                CutComplete();
        }
        
        void CutComplete()
        {
            SetCollidersEnabled(false);
            Invoke(nameof(DelayRetract), retractDelay);
            OnCutComplete.Invoke();
        }
        
        void DelayRetract()
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
        
        public void ActiveReset(bool forward)
        {
            if (!forward || !collidersEnabled)
                return;
                
            activeIndex = 1;
            hitStatus.Input(activeIndex);
        }
    }
}
