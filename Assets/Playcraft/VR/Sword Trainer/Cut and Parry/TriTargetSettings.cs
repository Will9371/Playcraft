using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class TriTargetSettings : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] float rotationSpeed;
        [SerializeField] float timeToExtend;
        [SerializeField] float timeToRetract;
        [SerializeField] float delayExtendTime;
        [SerializeField] float delayRotationTime;
        [SerializeField] float spreadDistance;
        [SerializeField] float targetDepth;
        [SerializeField] float targetScale = 1f;
        [SerializeField] FloatArray angles;
        
        [Header("References")]
        [SerializeField] RotateToAngle rotor;
        [SerializeField] GetFloatFromArray angler;
        [SerializeField] GetPercentOverTimeMono extension;
        [SerializeField] GetPercentOverTimeMono retraction;
        [SerializeField] TimedEvent delayExtend;
        [SerializeField] TimedEvent delayRotate;
        [SerializeField] LerpPositionMono enterTarget;
        [SerializeField] LerpPositionMono exitTarget;
        [SerializeField] Transform[] targets;
        
        void OnValidate() { Refresh(); }
        
        void Refresh()
        {
            rotor.SetRotationSpeed(rotationSpeed);
            angler.SetValues(angles);
            
            extension.SetDuration(timeToExtend);
            retraction.SetDuration(timeToRetract);
            if (delayExtend) delayExtend.SetTime(delayExtendTime);
            delayRotate.SetTime(delayRotationTime);
            
            enterTarget.end = new Vector3(spreadDistance/2f, 0f, targetDepth);
            exitTarget.end = new Vector3(-spreadDistance/2f, 0f, targetDepth);
            
            var scale = new Vector3(targetScale, targetScale, targetScale);
            foreach (var target in targets)
                target.localScale = scale;
        }
    }
}
