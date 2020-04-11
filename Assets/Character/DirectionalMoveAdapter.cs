using UnityEngine;

public class DirectionalMoveAdapter : MonoBehaviour
{
    MoveController movement;
    
    private void Awake()
    {
        movement = GetComponent<MoveController>();
    }
    
    public void MoveForward()
    {
        movement.AddMovement(Vector3.forward);
    }
    
    public void MoveLeft()
    {
        movement.AddMovement(Vector3.left);
    }
    
    public void MoveBack()
    {
        movement.AddMovement(Vector3.back);
    }
    
    public void MoveRight()
    {
        movement.AddMovement(Vector3.right);
    }
    
    public void TurnLeft()
    {
        movement.AddRotation(Axis.Y, false);
    }
    
    public void TurnRight()
    {
        movement.AddRotation(Axis.Y, true);
    }
}
