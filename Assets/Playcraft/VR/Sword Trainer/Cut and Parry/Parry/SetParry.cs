using System.Collections;
using UnityEngine;

// RENAME: TriParry
namespace Playcraft.Examples.SwordTrainer
{
    public class SetParry : MonoBehaviour, ISwordAction, ISwordTrainerTarget
    {
        [SerializeField] SwordActionId _actionId = SwordActionId.Parry;
        public SwordActionId actionId => _actionId;
    
        [SerializeField] ParryTargetOrbState[] orbs;
        [SerializeField] LerpPositionIndex movement;
        [SerializeField] LerpRotationIndex rotation;
        [SerializeField] GetPercentOverTime transitionTimer;
        [SerializeField] GetPercentOverTime holdTimer;

        [SerializeField] Vector3Array positionData;
        [SerializeField] Vector3Array rotationData;
        
        [SerializeField] FloatEvent outputHoldPercent;
        [SerializeField] BoolEvent outputSuccess;
        
        IPercent[] moveTransitions = new IPercent[2];

        int uniqueParryCount => movement.positions.Length;
        
        void Start()
        {
            moveTransitions[0] = movement;
            moveTransitions[1] = rotation;
        
            movement.SetDestinations(positionData);
            rotation.SetDestinations(rotationData);
        }
        
        public void RefreshSettings(ScriptableObject value)
        {
            if (value is ParryTargetSettings settings)
                RefreshSettings(settings);
        }
        
        public void RefreshSettings(ParryTargetSettings settings)
        {
            movement.SetDestinations(settings.positionData);
            rotation.SetDestinations(settings.rotationData);
            holdTimer.duration = settings.holdTime;
            transitionTimer.duration = settings.transitionTime;
        }

        [HideInInspector]
        public bool inTransition;
        
        public bool hittable { get; private set; }

        IEnumerator Transition() 
        {
            inTransition = true;
            yield return StartCoroutine(Extend(false));
            SetRandomParry();
            yield return transitionTimer.Run(moveTransitions);
            yield return StartCoroutine(Extend(true));
            inTransition = false;
        }
        
        [HideInInspector] public int parryIndex;

        void SetRandomParry()
        {
            parryIndex = RandomStatics.RandomIndexNotIncluding(uniqueParryCount, parryIndex);
            rotation.SetDestination(parryIndex);
            movement.SetDestination(parryIndex);
        }

        public void Trigger() { BeginActivation(); }
        public void BeginActivation() 
        {
            if (!gameObject.activeSelf) return; 
            StartCoroutine(Activate()); 
        }
        
        public IEnumerator Activate()
        {
            yield return StartCoroutine(WaitForTransition());
        
            ActivateOrbs(true);
            yield return StartCoroutine(Hold());
            
            DeactivateAndBeginNext(false);
        }
        
        IEnumerator WaitForTransition()
        {
            while (inTransition)
                yield return null;
        }
        
        IEnumerator Hold()
        {
            holdTimer.Begin();
            while (holdTimer.inProgress)
            {
                outputHoldPercent.Invoke(holdTimer.percent);
                yield return null;
            }
            outputHoldPercent.Invoke(1f);
        }
        
        IEnumerator Extend(bool value)
        {
            foreach (var orb in orbs)
                orb.SetExtended(value);
                
            yield return new WaitForSeconds(orbs[0].extendTime);
        }

        public void DeactivateAndBeginNext(bool success)
        {
            Deactivate();
            StartCoroutine(Transition());
            outputSuccess.Invoke(success);
        }
        
        void OnDisable() { Deactivate(); }
        
        void Deactivate()
        {
            StopAllCoroutines();
            inTransition = false;
            outputHoldPercent.Invoke(0f);
            ActivateOrbs(false);
        }

        void ActivateOrbs(bool value)
        {
            hittable = value;
            
            foreach (var orb in orbs)
                orb.SetReadyToParry(value);
        }
        
        public void SetActive(bool value) { gameObject.SetActive(value); }
        public void SetLocalPosition(Vector3 value) { transform.localPosition = value; }
        
        
    }
}
