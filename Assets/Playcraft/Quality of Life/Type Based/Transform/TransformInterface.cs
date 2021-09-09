using UnityEngine;

public class TransformInterface : MonoBehaviour
{
    public Transform value;
    public Vector3 position => value.position;
}
