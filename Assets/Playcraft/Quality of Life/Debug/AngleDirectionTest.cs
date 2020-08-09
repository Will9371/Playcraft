using Playcraft;
using UnityEngine;

public class AngleDirectionTest : MonoBehaviour
{
    [SerializeField] Vector2 direction;
    [SerializeField] Vector2 zeroDirection = Vector2.right;
    [SerializeField] float angle;
    [SerializeField] bool setAngleByDirection;
    [SerializeField] bool setDirectionByAngle;


    private void OnValidate()
    {
        if (setAngleByDirection)
        {
            angle = VectorMath.Vector2ToDegree(direction.normalized, zeroDirection.normalized);
            setAngleByDirection = false;
        }
        
        if (setDirectionByAngle)
        {
            direction = VectorMath.DegreeToVector2(angle, zeroDirection.normalized);
            setDirectionByAngle = false;
        }
    }
}
