using System;
using UnityEngine;

namespace Playcraft
{
    public class RotateTowards : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] float speed = 5000;
        
        [NonSerialized] public float angle;
        
        Quaternion lookRotation;
        Vector3 direction;
        float step;
            
        public void SetDirection(Vector3SO value) { SetDirection(value.value); }
        public void SetDirection(Vector3 value) { direction = value; }
        
        void Start()
        {
            if (!self) self = transform;
        }

        void Update()
        {
            if (direction == Vector3.zero) 
                direction = self.forward;
            
            step = speed * Time.deltaTime;
            lookRotation = Quaternion.LookRotation(direction);
            self.rotation = Quaternion.RotateTowards(self.rotation, lookRotation, step);
            
            angle = Vector3.Angle(self.forward, direction);            
        }
        
        public void SetDirectionInstant(Vector3 direction)
        {
            self.rotation = Quaternion.LookRotation(direction);   
        }
    }
}
