using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    void LateUpdate()
    {
        transform.position = target.position;
        transform.rotation = target.rotation;
    }
}
