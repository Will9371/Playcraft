using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] bool invertX;
    [SerializeField] bool invertY = true;
    private float x, y;

    public void Rotate(Vector2 value)
    {
        x += value.y * (invertY ? -1f : 1f);
        y += value.x * (invertX ? -1f : 1f);
        transform.eulerAngles = new Vector3(x, y, 0);
    }
}
