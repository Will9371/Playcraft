using UnityEngine;

public class TeleportPoint : MonoBehaviour
{
    [SerializeField] Transform point;
    public Vector3 GetPoint() { return point.position; }
}
