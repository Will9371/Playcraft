using System;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    [Serializable] public class TurnAxisEvent : UnityEvent<Axis, bool> { }

    public class InputRotation : MonoBehaviour
    {    
        #pragma warning disable 0649
        [SerializeField] Vector3Event OnRotate;
        #pragma warning restore 0649
        
        Vector3 rotationAxis;

        //public void AddRotation(TurnDirection turn) { AddRotation(turn.axis, turn.clockwise); }
        //public void AddRotation(Axis axis, bool clockwise) { AddRotation(StaticAxis.GetAxisVector(axis, clockwise)); }
        public void AddRotation(Vector3SO direction) { AddRotation(direction.value); }
        public void AddRotation(Vector3 rotation) { rotationAxis += rotation; }
        
        
        private void Update()
        {
            rotationAxis = rotationAxis.normalized;
            OnRotate.Invoke(rotationAxis);
            rotationAxis = Vector3.zero; 
        }
    }
}
