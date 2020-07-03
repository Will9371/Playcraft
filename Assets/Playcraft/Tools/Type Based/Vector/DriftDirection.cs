using Playcraft;
using UnityEngine;

// Assumes flattened direction
public class DriftDirection : MonoBehaviour
{
    [SerializeField] bool debug;
    [SerializeField] float turnSpeed = 1f;

    Vector3 direction;
    Vector3 desiredDirection;
    public void SetDesiredDirection(Vector3 value) { desiredDirection = value; }
    
    [SerializeField] Vector3Event Output;
    
    private void Update()
    {
        if (direction == Vector3.zero) direction = desiredDirection;
                
        var c = VectorMath.Vector3to2(direction);
        var d = VectorMath.Vector3to2(desiredDirection);
        c = VectorMath.RotateTowards(c, d, turnSpeed, Time.deltaTime);
        direction = VectorMath.Vector2to3(c);
        
        Output.Invoke(direction);
    }
    
    private void OnDrawGizmos()
    {
        if (!debug) return;
        DrawRayFromSelf(direction, 3f, Color.red);
        DrawRayFromSelf(desiredDirection, 3f, Color.green);
    }
    
    private void DrawRayFromSelf(Vector3 direction, float distance, Color color)
    {
        Gizmos.color = color;
        Gizmos.DrawRay(transform.position, direction * distance);
    }
}
