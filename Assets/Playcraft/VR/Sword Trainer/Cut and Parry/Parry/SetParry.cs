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
        float transitionTime => transitionTimer.duration;
        
        void Start()
        {
            movement.SetDestinations(positionData);
            rotation.SetDestinations(rotationData);
        }

        public void SetRandomParry(bool readyOnArrive) { StartCoroutine(Transition(readyOnArrive)); }
        
        IEnumerator Transition(bool readyOnArrive)
        {
            var nextParryIndex = Random.Range(0, uniqueParryCount);
            //Debug.Log($"SetRandomParry: {nextParryIndex} at time {Time.time}");
            rotation.SetDestination(nextParryIndex);
            movement.SetDestination(nextParryIndex);
            
            transitionTimer.Begin();
            
            while (transitionTimer.inProgress)
            {
                movement.Input(transitionTimer.percent);
                rotation.Input(transitionTimer.percent);
                yield return null;
            }
            
            movement.Input(1f);
            rotation.Input(1f);
            
            if (readyOnArrive)
                SetParryReady();          
        }
        
        public void SetParryReady() { StartCoroutine(Hold()); }
        
        IEnumerator Hold()
        {
            yield return StartCoroutine(Extend(true));
            ActivateOrbs(true);
            
            holdTimer.Begin();
            while (holdTimer.inProgress)
            {
                yield return null;
                outputHoldPercent.Invoke(holdTimer.percent);
            }
            
            Deactivate(false);
        }
        
        IEnumerator Extend(bool value)
        {
            foreach (var orb in orbs)
                orb.SetExtended(value);
                
            yield return new WaitForSeconds(orbs[0].extendTime);
        }
        
        public void Deactivate(bool success)
        {
            StopAllCoroutines();
            outputHoldPercent.Invoke(0f);
            ActivateOrbs(false);
            StartCoroutine(Extend(false));
            outputSuccess.Invoke(success);
        }
        
        public void ActivateOrbs(bool value)
        {
            foreach (var orb in orbs)
                orb.SetReadyToParry(value);
        }
    }
}
