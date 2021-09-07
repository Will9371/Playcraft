using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class ParrySetup : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float transitionTime;
        [SerializeField] Vector3Array parryPositions;
        [SerializeField] Vector3Array parryRotations;
        
        [Header("References")]
        [SerializeField] SetParry controller;
        [SerializeField] LerpPositionIndexMono movement;
        [SerializeField] LerpRotationIndexMono rotation;
        [SerializeField] GetPercentOverTimeMono timer;
        
        void Awake()
        {
            controller.Inject(transitionTime, movement, rotation, timer);
            timer.SetDuration(transitionTime);
            movement.SetDestinations(parryPositions);
            rotation.SetDestinations(parryRotations);
        }
    }
}
