using System.Collections;
using UnityEngine;

// RENAME
namespace Playcraft.Examples.SwordTrainer
{
    public class SetParry : MonoBehaviour
    {
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

        IEnumerator Transition() 
        {
            inTransition = true;
            yield return StartCoroutine(Extend(false));
            SetRandomParry();
            yield return StartCoroutine(MoveToNextLocation());
            yield return StartCoroutine(Extend(true));
            inTransition = false;
        }
        
        void SetRandomParry()
        {
            var nextParryIndex = Random.Range(0, uniqueParryCount);
            rotation.SetDestination(nextParryIndex);
            movement.SetDestination(nextParryIndex);
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
        
        public void BeginActivation() { StartCoroutine(Activate()); }
        
        public IEnumerator Activate()
        {
            yield return StartCoroutine(WaitForTransition());
        
            ActivateOrbs(true);
            yield return StartCoroutine(Hold());
            
            Deactivate(false);
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

        void Deactivate(bool success)
        {
            StopAllCoroutines();
            
            outputHoldPercent.Invoke(0f);
            ActivateOrbs(false);
            
            StartCoroutine(Transition());
            outputSuccess.Invoke(success);
        }
        
        void ActivateOrbs(bool value)
        {
            foreach (var orb in orbs)
                orb.SetReadyToParry(value);
        }
        
        public void SetActive(bool value) { gameObject.SetActive(value); }
    }
}
