using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Playcraft.Examples.SwordTrainer
{
    public class DirectionalCutTarget : MonoBehaviour, ISwordAction, ISwordTrainerTarget
    {
        [SerializeField] SwordActionId _actionId = SwordActionId.Cut;
        public SwordActionId actionId => _actionId;
    
        [SerializeField] float vulnerableDuration;
        [SerializeField] float spinDuration;
        [SerializeField] float retractDuration;
        [SerializeField] LerpAxisAngle rotor;
        [SerializeField] LerpPosition countdown;
        [SerializeField] Renderer indicator;
        [SerializeField] RelayCutScoreAndDirection hitbox;
        [SerializeField] Color vulnerableColor = Color.green;
        [SerializeField] Color invulnerableColor = Color.red;
        [SerializeField] LerpPosition setHeight;
        [SerializeField] TargetArea[] targetAreas;
        [SerializeField] UnityEvent OnComplete;
        
        GetPercentOverTime timer = new GetPercentOverTime();
        IPercent[] transitionLerps = new IPercent[2];
        
        public bool hittable => !hitbox.invulnerable;

        public void SetActive(bool value) { gameObject.SetActive(value); }
        public void SetLocalPosition(Vector3 value) { transform.localPosition = value; }
        public void RefreshSettings(ScriptableObject value) { }
        
        void Start()
        {
            Initialize();
            SetVulnerable(false);
        }
        
        bool initialized;
        
        void Initialize()
        {
            if (initialized) return;
            transitionLerps[0] = rotor;
            transitionLerps[1] = setHeight;
            initialized = true;            
        }
        
        public void Trigger() 
        {
            Debug.Log("Trigger!");
            Initialize(); 
            StartCoroutine(Routine()); 
        }

        IEnumerator Routine()
        {
            yield return Spin();
            yield return Extend(true, vulnerableDuration);
            yield return Extend(false, retractDuration);
            OnComplete.Invoke();
        }
        
        IEnumerator Spin()
        {
            var heightIndex = Random.Range(0, targetAreas.Length);
            var targetArea = targetAreas[heightIndex];
            setHeight.SetEnd(targetArea.position);
        
            var angleIndex = Random.Range(0, targetArea.cutAngles.values.Length);
            var angleDestination = targetArea.cutAngles.values[angleIndex];
            rotor.SetDestination(angleDestination);
            
            yield return timer.Run(transitionLerps, spinDuration);
        }
        
        IEnumerator Extend(bool moveIn, float duration)
        {
            SetVulnerable(moveIn);
            countdown.reverse = !moveIn;
            yield return timer.Run(countdown, duration);
        }
        
        void SetVulnerable(bool value)
        {
            indicator.material.color = value ? vulnerableColor : invulnerableColor;
            hitbox.SetInvulnerable(!value);
        }
        
        [Serializable]
        public struct TargetArea
        {
            public Vector3 position;
            public FloatArray cutAngles;
        }
    }
}
