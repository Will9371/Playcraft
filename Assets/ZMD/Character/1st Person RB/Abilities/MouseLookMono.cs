using System;
using UnityEngine;

namespace ZMD.FPS
{
    public class MouseLookMono : MonoBehaviour
    {
        public MouseLook process;
        void Update() { process.Update(); }
        void OnValidate() { process.OnValidate(); }
    }
    
    [Serializable]
    public class MouseLook
    {
        [Header("References")]
        public Transform bodyPivot;
        public Transform cameraPivot;
    
        [Header("Settings")]
        public Vector2 upDownLookRange = new Vector2(-80f, 80f);
        public bool invertVertical = true;

        public GetMouseMovement mouse;
        [HideInInspector] public SingleAxisRotation bodyRotor;
        [HideInInspector] public SingleAxisRotation cameraRotor;

        public void Update()
        {
            var mouseMovement = mouse.Update();
            bodyRotor.Rotate(mouseMovement.x);
            cameraRotor.Rotate(mouseMovement.y);
        }
        
        #region Validation
        
        public void OnValidate()
        {
            ValidateBody();
            ValidateCamera();
        }
        
        void ValidateBody()
        {
            if (!bodyPivot) return;
            bodyRotor.self = bodyPivot;
            bodyRotor.rotationAxis = Axis.Y;
            bodyRotor.invert = false;
            bodyRotor.clamp = false;
            bodyRotor.range = Vector2.zero;            
        }
        
        void ValidateCamera()
        {
            if (!cameraPivot) return;
            cameraRotor.self = cameraPivot;
            cameraRotor.rotationAxis = Axis.X;
            cameraRotor.invert = invertVertical;
            cameraRotor.clamp = true;
            cameraRotor.range = upDownLookRange;            
        }  
        
        #endregion      
    }
}