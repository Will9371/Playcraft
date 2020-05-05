using UnityEngine;

// REFACTOR: seperate rotation from movement, generalize naming
public class Flier : MonoBehaviour
{
    [SerializeField] float startSpeed = 5f;
    [SerializeField] float minSpeed = 1f;
    [SerializeField] float maxSpeed = 20f;
    float flySpeed;
    
    [SerializeField] bool invertX;
    [SerializeField] bool invertY = true;
    private float x, y;
    
    private void Start()
    {
        flySpeed = startSpeed;
    }
    
    public void Rotate(Vector2 value)
    {
        x += (value.y * (invertY ? -1f : 1f));
        y += (value.x * (invertX ? -1f : 1f));
        transform.eulerAngles = new Vector3(x, y, 0);
    }
    
    public void Move(Vector3SO data) { Move(data.value); }
    
    public void Move(Vector3 direction)
    {
        var step = direction * flySpeed * Time.deltaTime;
        transform.Translate(step);
    }
    
    public void Accellerate(float value)
    {
        flySpeed += value;
        if (flySpeed < minSpeed) flySpeed = minSpeed;
        if (flySpeed > maxSpeed) flySpeed = maxSpeed;
    }
}
