using System;
using UnityEngine;

namespace ZMD
{
    public class KeepDistanceMono : MonoBehaviour
    {
        [SerializeField] KeepDistance process;
        void Update() { process.Update(); }
    }

    /// Move away from a target if it gets within a minimum distance
    [Serializable] 
    public class KeepDistance
    {
        public Transform self;
        public Transform target;
        public float minDistance = 1f;
        public float speed = 1f;  
        public bool lockY;
        public Space space = Space.Self;
        
        Vector3 position => self.position;
        
        float distance; 
        Vector3 targetVector;
        Vector3 targetDirection;
        Vector3 step;
        
        public void Update()
        {
            if (!self || !target) return;
            
            distance = Vector3.Distance(position, target.position);
            if (distance >= minDistance) return;
            
            targetVector = target.position - position;
            if (lockY) targetVector -= Vector3.up * targetVector.y;
            targetDirection = targetVector.normalized;
            
            step = speed * Time.deltaTime * -targetDirection;
            self.Translate(step, space);
        } 
    }
}
