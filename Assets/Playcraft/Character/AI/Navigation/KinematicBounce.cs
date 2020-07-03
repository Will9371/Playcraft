using Playcraft;
using UnityEngine;
using Random = UnityEngine.Random;

public class KinematicBounce : MonoBehaviour
{
    [SerializeField] bool bounceX, bounceY, bounceZ;
    [SerializeField] Vector3Event Output;
    
    Vector3 direction;
    public void SetDirection(Vector3 value) { direction = value; }

    /*#region Initialization (refactor out)
    private void Start()
    {
        direction = GetRandomDirection();
    }
    
    private Vector3 GetRandomDirection()
    {
        var flatDirection = Random.insideUnitCircle.normalized;
        return VectorMath.Vector2to3(flatDirection);        
    }
    #endregion*/
    
    private void OnTriggerEnter(Collider other)
    {
        Vector3 bounceDirection = new Vector3();
        bounceDirection.x = bounceX ? -direction.x : direction.x;
        bounceDirection.y = bounceY ? -direction.y : direction.y;
        bounceDirection.z = bounceZ ? -direction.z : direction.z;
        SetDirection(bounceDirection);
        Output.Invoke(direction); 
    }
}
