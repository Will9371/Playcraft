using UnityEngine;

public class FaceTargetInstant : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] bool lookAway;
    
    [SerializeField] Transform target;
    public void SetTarget(Transform value) { target = value; }
    
    Vector3 targetDirection => (target.position - self.position).normalized;
    Vector3 lookDirection => lookAway ? -targetDirection : targetDirection;
    
    void Start() { if (!self) self = transform; }

    void Update()
    {
        if (!target) return;
        self.transform.rotation = Quaternion.LookRotation(lookDirection);
    }
}
