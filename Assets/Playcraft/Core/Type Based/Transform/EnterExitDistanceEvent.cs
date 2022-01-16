using UnityEngine;
using UnityEngine.Events;

public class EnterExitDistanceEvent : MonoBehaviour
{
    public Transform self;
    [SerializeField] bool setCenterToSelfOnStart;
    public Vector3 center;
    public float radius;
    [SerializeField] UnityEvent Enter;
    [SerializeField] UnityEvent Exit;
    
    float distanceFromCenter => Vector3.Distance(center, self.position);
    
    bool isInRange;
    bool wasInRange = true;
    
    void Start()
    {
        if (!self) self = transform;
        if (setCenterToSelfOnStart) center = self.position;        
    }

    void Update()
    {
        isInRange = distanceFromCenter <= radius;
        if (isInRange == wasInRange) return;
        
        if (!wasInRange && isInRange)
            Enter.Invoke();
        else if (wasInRange && !isInRange)
            Exit.Invoke();
            
        wasInRange = isInRange;
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(center, radius);
    }
}
