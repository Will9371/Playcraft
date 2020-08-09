using UnityEngine;

public class MoveFixed : MonoBehaviour
{
    [SerializeField] Vector3 direction;
    public void SetDirection(Vector3 value) { direction = value; }
    
    [SerializeField] float speed;
    public void SetSpeed(float value) { speed = value; }

    void Update()
    {
        if (direction == Vector3.zero || speed == 0) return;
        transform.Translate(direction * speed * Time.deltaTime);
    }
}
