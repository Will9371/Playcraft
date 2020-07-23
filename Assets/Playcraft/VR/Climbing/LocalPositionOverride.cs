using UnityEngine;

// HACK
public class LocalPositionOverride : MonoBehaviour
{
    [SerializeField] Vector3 forcePosition;
    [SerializeField] Quaternion forceRotation;
    
    void LateUpdate()
    {
        transform.localPosition = forcePosition;
        transform.localRotation = forceRotation;
    }
}
