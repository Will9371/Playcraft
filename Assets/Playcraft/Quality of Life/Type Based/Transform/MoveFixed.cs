using UnityEngine;

public class MoveFixed : MonoBehaviour
{
    [SerializeField] Transform self;

    [SerializeField] Vector3 direction;
    public void SetDirection(Vector3 value) { direction = value; }
    
    [SerializeField] float speed;
    public void SetSpeed(float value) { speed = value; }
    
    void Start()
    {
        if (!self) self = transform;
    }

    void Update()
    {
        if (direction == Vector3.zero || speed == 0) return;
        self.Translate(speed * Time.deltaTime * direction);
    }
}
