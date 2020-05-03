using System;
using UnityEngine;
using UnityEngine.Events;

public class InputMovement : MonoBehaviour
{    
    public float rotationSpeed;
    
    [SerializeField] Vector3Event OnMove;
    [SerializeField] Vector3Event OnRotate;
            
    private Vector3 moveInput;
    Vector3 rotationAxis;
    
    public void AddMovement(Vector3SO direction) { AddMovement(direction.value); }  
    public void AddMovement(Vector3 direction) { moveInput += direction; }
    
    public void AddRotation(TurnDirection turn) { AddRotation(turn.axis, turn.clockwise); }
    public void AddRotation(Axis axis, bool clockwise) { AddRotation(StaticAxis.GetAxisVector(axis, clockwise)); }
    public void AddRotation(Vector3 rotation) { rotationAxis += rotation; }
    
    private void Update()
    {
        Move();
        Rotate();
    }
    
    private void Move()
    {
        moveInput = moveInput.normalized;
        OnMove.Invoke(moveInput);
        moveInput = Vector3.zero; 
    }
    
    private void Rotate()
    {
        rotationAxis = rotationAxis.normalized;
        var rotationStep = rotationAxis.magnitude * rotationSpeed * Time.deltaTime;
        transform.Rotate(rotationAxis, rotationStep);
        OnRotate.Invoke(rotationAxis);
        rotationAxis = Vector3.zero; 
    }
}

[Serializable] public class TurnAxisEvent : UnityEvent<Axis, bool> { }
