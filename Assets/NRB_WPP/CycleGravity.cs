using UnityEngine;
using Playcraft;

public class CycleGravity : MonoBehaviour
{
    [SerializeField] NonRigidbodyMovement movement;
    public Vector3[] gravityDirections;
    
    int index;
    
    public void Cycle(bool clockwise)
    {
        index = RangeMath.CycleInt(index, gravityDirections.Length - 1, clockwise);
    }
    
    public void SetActive(bool value)
    {
        movement.gravity = value ? gravityDirections[index] : Vector3.zero;
    }
}
