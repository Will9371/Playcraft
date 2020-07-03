using System;
using UnityEngine;

namespace Playcraft.Examples.ColorCatcher
{
    public class MoveBlocker : MonoBehaviour
    {
        LerpSingleton moveController => LerpSingleton.instance;
    
        #pragma warning disable 0649
        [SerializeField] WispBlockerOverride self;  
        [SerializeField] SwapWispBlockerData controller;
        #pragma warning restore 0649
                
        Vector3 toPoint;
        int toIndex;
        Action endMethod; 
        
        private void Awake()
        {
            endMethod = EndSlide;    
        }
        
        public void BeginSlide(WispBlockerOverride destination, float slideTime)
        {            
            toPoint = destination.data.restPosition; 
            toIndex = destination.data.index;
            moveController.BeginMove(transform, toPoint, slideTime, endMethod);            
        }
        
        private void EndSlide()
        {
            self.data.index = toIndex;
            self.data.restPosition = toPoint;
            controller.EndSlide();
        }
    }
}
