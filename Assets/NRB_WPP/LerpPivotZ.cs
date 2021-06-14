using UnityEngine;
using Playcraft;

public class LerpPivotZ : MonoBehaviour
{
    public float rotationDegrees = 120f;

    float priorAngle;
    float targetAngle;

    public void SetTargetRotation(bool clockwise)
    {
         priorAngle = transform.localEulerAngles.z;
         targetAngle = priorAngle + rotationDegrees * (clockwise ? -1 : 1);
    }
    
    public void SetRotation(float percent)
    {
        float z = Mathf.Lerp(priorAngle, targetAngle, percent);
        VectorMath.SetZRotation(transform, z);
    }
}
