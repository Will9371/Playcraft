using UnityEngine;
using Playcraft;

public class CircleTarget : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] Vector2 speedRange;
    [SerializeField] bool randomizeOnEnable;
    
    [SerializeField] Transform target;
    public void SetTarget(Transform value) { target = value; }
    
    float circleDirection = 1f;
    float speed;
    
    Vector3 planarTargetPosition => new Vector3(target.position.x, self.position.y, target.position.z);
    Vector3 targetDirection => (planarTargetPosition - self.position).normalized;
    Vector3 cross => Vector3.Cross(targetDirection, Vector3.up);
    Vector3 step => speed * circleDirection * Time.deltaTime * cross;
    
    void Start() { if (!self) self = transform; }

    void Update()
    {
        if (!target) return;        
        self.Translate(step);
    }
    
    void OnEnable()
    {
        if (randomizeOnEnable)
        {
            RandomizeDirection();
            RandomizeSpeed();
        }
    }
    
    public void RandomizeDirection()
    {
        circleDirection = RandomStatics.CoinToss() ? 1f : -1f; 
    }
    
    public void RandomizeSpeed()
    {
        speed = Random.Range(speedRange.x, speedRange.y);
    }
}
