using System;
using UnityEngine;

namespace ZMD.FPS
{
    public class CrouchMono : MonoBehaviour
    {
        public Crouch process;

        void OnValidate() 
        { 
            process.mono = this; 
            process.OnValidate();
        }
        
        public void CrouchKeyPressed() { process.CrouchKeyPressed(); }
    }

    [Serializable]
    public class Crouch
    {
        [ReadOnly] public MonoBehaviour mono;

        [ReadOnly] public bool isCrouched;
        
        public float transitionDuration = 0.25f;
        public bool useCurve;
        public AnimationCurve curve;
        public LerpPositionOverTime moveEyes;
        public LerpScaleOverTime scaleTorso;
        public LerpCapsuleHeightOverTime scaleCollider;
        
        ILerpOverTime[] subprocesses;
        
        public void OnValidate()
        {
            subprocesses = new ILerpOverTime[] { moveEyes, scaleTorso, scaleCollider };
            
            foreach (var subprocess in subprocesses)
            {
                subprocess.SetDuration(transitionDuration);
                subprocess.useCurve = useCurve;
                subprocess.curve = curve;
            }
        }

        public void CrouchKeyPressed() { SetCrouch(!isCrouched); }
        
        public void SetCrouch(bool value)
        {
            if (value == isCrouched) return;
            
            foreach (var subprocess in subprocesses)
                subprocess.FlipAndRun(mono);
                
            isCrouched = value;
        }
    }
}
