using System;
using UnityEngine;

namespace Playcraft
{
    public class RotateTowards : MonoBehaviour
    {
        [SerializeField] float speed = 5000;
        
        [NonSerialized] public float angle;
        
        Quaternion lookRotation;
        Vector3 direction;
        float step;
            
        public void SetDirection(Vector3SO value) { SetDirection(value.value); }
        public void SetDirection(Vector3 value) { direction = value; }

        void Update()
        {
            step = speed * Mathf.Deg2Rad * Time.deltaTime;
            lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, step);
            angle = Vector3.Angle(transform.forward, direction);
        }
    }
}
