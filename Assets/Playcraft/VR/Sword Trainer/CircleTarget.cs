using UnityEngine;
using Playcraft;

public class CircleTarget : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] Vector2 speedRange;    // Rename
    [SerializeField] bool randomizeOnEnable;
    [SerializeField] Vector3Event Output;
    
    [SerializeField] Transform target;
    public void SetTarget(Transform value) { target = value; }
    
    float circleDirection = 1f;
    float weight;
    
    Vector3 targetDirection => (planarTargetPosition - self.position).normalized;
    Vector3 cross => Vector3.Cross(targetDirection, Vector3.up);
    Vector3 sideDirection => circleDirection * cross;
        
    
    void Start() { if (!self) self = transform; }
    
    Vector3 planarTargetPosition;

    void Update()
    {
        if (!target) return;   
        planarTargetPosition = new Vector3(target.position.x, self.position.y, target.position.z);
        Output.Invoke(sideDirection * weight);     
    }
    
    void OnEnable()
    {
        if (randomizeOnEnable)
        {
            RandomizeDirection();
            RandomizeSpeed();
        }
    }
    
    public void RandomizeDirection() { circleDirection = RandomStatics.CoinToss() ? 1f : -1f;  }
    
    // Rename
    public void RandomizeSpeed() { weight = Random.Range(speedRange.x, speedRange.y); }
}
