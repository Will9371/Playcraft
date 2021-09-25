using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class CutTarget : MonoBehaviour, ISwordAction
    {
        [SerializeField] float retractDelay;
        [SerializeField] bool activateOnStart;
        [SerializeField] BoolEvent OnCutComplete;
        [SerializeField] BoolEvent OnRetractComplete;
        
        [SerializeField] Collider[] colliders;
        [SerializeField] GetPercentOverTimeMono extend;
        [SerializeField] GetPercentOverTimeMono retract;
        [SerializeField] DisplaySequenceByColor hitStatus;
        
        //[SerializeField] SetRespondToCustomTagsArray cutTags;
        [SerializeField] GameObject[] barriers;

        int activeIndex;
        bool success;
        
        public bool hittable => collidersEnabled;
        
        void Start()
        {
            if (!activateOnStart) return;
            Hit(0);
            extend.Begin();
        }

        public void Trigger() { BeginExtension(); }
        public void BeginExtension() { if(gameObject.activeSelf) extend.Begin(); }
        
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
            this.success = success;
            OnCutComplete.Invoke(success);
        }
        
        void Retract()
        {
            activeIndex = 0;
            retract.Begin();
            hitStatus.Input(activeIndex);
            Invoke(nameof(RetractComplete), retract.GetDuration());         
        }
        
        void RetractComplete() { OnRetractComplete.Invoke(success); }
        
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
        
        public void SetActive(bool value) { gameObject.SetActive(value); }
        
        public void SetLocalPosition(Vector3 value) { transform.localPosition = value; }

        //public void SetTriggerTags(int groupIndex) { cutTags.SetTriggerTags(groupIndex); }
        
        public void SetBarriersActive(bool value)
        {
            foreach (var barrier in barriers)
                barrier.SetActive(value);
        }
    }
}
