using System;
using UnityEngine;

namespace ZMD
{
    public class MovePhysicsMono : MonoBehaviour, IAddForce
    {
        public MovePhysics process;

        void Update() { process.Update(); }
        
        void OnValidate()
        {
            process.mono = this;
            process.rb = GetComponent<Rigidbody>();
        }

        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Impulse, float duration = 0.5f) 
            => process.AddForce(force, mode);
    }
    
    [Serializable]
    public class MovePhysics
    {
        [Header("References")]
        public MonoBehaviour mono;
        public Rigidbody rb;
        
        [Header("Settings")]
        public float baseSpeed = 3f;
        
        [ReadOnly] public bool stateSlide;
        float priorSpeedMultiplier;
        [NonSerialized] public float externalSpeedMultiplier;
        float speedMultiplier => slide ? priorSpeedMultiplier : externalSpeedMultiplier;
        
        bool forceSlide;
        bool slide => stateSlide || forceSlide;
        float speed => baseSpeed * speedMultiplier;
        
        [Header("Subprocesses")]
        public KeyboardInputToVector keyboard;

        Vector3 inputDirection;
        Vector3 priorDirection;
        Vector3 direction => slide ? priorDirection : inputDirection;

        Vector3 horizontal;
        Vector3 velocity;
        
        public void Update()
        {
            inputDirection = keyboard.Update();
        
            horizontal = mono.transform.TransformVector(direction * speed);
            velocity = slide ? rb.velocity : new Vector3(horizontal.x, rb.velocity.y, horizontal.z);
            
            rb.velocity = velocity;
            
            if (!slide)
            {
                priorSpeedMultiplier = externalSpeedMultiplier;
                priorDirection = inputDirection;
            }
        }
        
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Force, float duration = 0.5f) 
        { 
            rb.AddForce(force, mode);
            forceSlide = true;
            mono.Invoke(nameof(EndForceSlide), duration);
        }
        
        void EndForceSlide() { forceSlide = false; }
        
        /*#region Slide
        
        [Tooltip("When any elements in this list are active, character will slide.  " +
                 "To prevent loss of control, remove condition from array.")]
        [SerializeField] PotentialBoolEffects slideCauses;
        
        bool slide => slideCauses.anyActive;
        
        public void ActivateSlide(SO cause) { slideCauses.SetEffectActive(cause, true); }
        public void DeactivateSlide(SO cause) { slideCauses.SetEffectActive(cause, false); }
        
        public void SetSlideForTime(SO cause, float duration) 
        { slideCauses.SetEffectActiveForTime(cause, duration); }
        
        #endregion*/        
    }
}
