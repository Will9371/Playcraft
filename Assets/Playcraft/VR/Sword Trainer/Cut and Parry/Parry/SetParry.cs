using System.Collections;
using UnityEngine;

// RENAME
namespace Playcraft.Examples.SwordTrainer
{
    public class SetParry : MonoBehaviour, ISwordAction, ISwordTrainerTarget
    {
        [SerializeField] SwordActionId _actionId;
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

        int uniqueParryCount => movement.positions.Length;
        public float holdTime => holdTimer.duration;
        public float transitionTime => transitionTimer.duration;
        
        void Start()
        {
            movement.SetDestinations(positionData);
            rotation.SetDestinations(rotationData);
        }

        [HideInInspector]
        public bool inTransition;
        
        public bool hittable { get; private set; }

        IEnumerator Transition() 
        {
            inTransition = true;
            yield return StartCoroutine(Extend(false));
            SetRandomParry();
            yield return StartCoroutine(MoveToNextLocation());
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
        
        IEnumerator MoveToNextLocation()
        {
            transitionTimer.Begin();
            while (transitionTimer.inProgress)
            {
                movement.Input(transitionTimer.percent);
                rotation.Input(transitionTimer.percent);
                yield return null;
            }
            movement.Input(1f);
            rotation.Input(1f);           
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
        
        public void SetActive(int index, int activeCount) 
        { 
            gameObject.SetActive(index < activeCount); 
        }
        
        public void SetLocalPosition(Vector3 value) { transform.localPosition = value; }
        
        void OnDisable() { Deactivate(); }
    }
}
