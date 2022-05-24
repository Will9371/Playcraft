using System;
using UnityEngine;

namespace ZMD.FPS
{
    public class HumanoidMovePhysicsMono : MonoBehaviour
    {
        public HumanoidMovePhysics process;
        
        void Start() { process.Start(); }
        void Update() { process.Update(); }
        void OnCollisionEnter(Collision other) { process.OnCollisionEnter(other); }
        void OnCollisionExit(Collision other) { process.OnCollisionExit(other); }
        void OnValidate() { process.OnValidate(this, GetComponent<Rigidbody>()); }
        
        public void JumpKeyPressed() { process.JumpKeyPressed(); }
        public void CrouchKeyPressed() { process.CrouchKeyPressed(); }
        public void RunKeyPressed(bool value) { process.RunKeyPressed(value); }
        
        public void AddForce(Vector3 force, ForceMode mode = ForceMode.Impulse, float duration = 0.5f) 
        { 
            process.movement.AddForce(force, mode); 
        }
    }

    [Serializable]
    public class HumanoidMovePhysics
    {
        public MovePhysics movement;
        public MouseLook look;
        public JumpPhysics jump;
        public Crouch crouch;
        public RespondToCustomTag waterDetection;

        public float walkSpeedMultiplier = 1f;
        public float runSpeedMultiplier = 2f;
        public float crouchSpeedMultiplier = 0.5f;
        public bool lockMovementInAir;
        [Range(0, 1)] public float waterSlowdown;
        
        [ReadOnly] public bool inWater;
        public bool isCrouched => crouch.isCrouched;
        
        public void Start()
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
        public void Update()
        {
            movement.externalSpeedMultiplier = GetMoveStateSpeed();
            movement.Update();
            look.Update();
        }
        
        public void OnCollisionEnter(Collision other) 
        { 
            jump.OnCollisionEnter(other); 
            
            if (jump.grounded)
                movement.stateSlide = false;
                
            if (waterDetection.Input(other.collider))
            {
                inWater = true;
                crouch.SetCrouch(false);
            }
        }
        
        public void OnCollisionExit(Collision other)
        {
            if (waterDetection.Input(other.collider))
                inWater = false;
        }

        public void CrouchKeyPressed() 
        {
            if (inWater) return; 
            crouch.CrouchKeyPressed(); 
        }
        
        public void JumpKeyPressed() 
        {
            if (inWater || isCrouched)  
                return;
             
            var success = jump.Jump();
            
            if (success && lockMovementInAir)
                movement.stateSlide = true;
        }
        
        bool runInput;
        public void RunKeyPressed(bool value) { runInput = value; } 

        float GetMoveStateSpeed()
        {
            if (inWater)
                return walkSpeedMultiplier * waterSlowdown;
            if (isCrouched)
                return crouchSpeedMultiplier;
            if (runInput)
                return runSpeedMultiplier;
                
            return walkSpeedMultiplier;
        }
        
        public void OnValidate(MonoBehaviour mono, Rigidbody rb)
        {
            movement.mono = mono;
            movement.rb = rb;
            
            jump.rb = rb;
            jump.OnValidate();
            
            look.bodyPivot = mono.transform;
            look.OnValidate();
            
            crouch.mono = mono;
            crouch.OnValidate();
        }
    }
}
