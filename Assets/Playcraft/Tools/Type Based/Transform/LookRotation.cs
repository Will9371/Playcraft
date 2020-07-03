using UnityEngine;

public class LookRotation : MonoBehaviour
{
    [SerializeField] Vector3 axis;

    public void Input(Vector3 value)
    {
        if (value == Vector3.zero) return;
        var rotation = Quaternion.LookRotation(value, axis);
        transform.rotation = rotation;
    }
}
