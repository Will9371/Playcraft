using UnityEngine;

namespace Playcraft
{
    public class FaceTargetInstant : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] bool lookAway;
        
        [SerializeField] Transform target;
        public void SetTarget(Transform value) { target = value; }
        
        [Header("Constraints")]
        [SerializeField] bool x;
        [SerializeField] bool y;
        [SerializeField] bool z;
        
        Vector3 targetDirection => (targetPosition - self.position).normalized;
        Vector3 lookDirection => lookAway ? -targetDirection : targetDirection;
        
        void Start() { if (!self) self = transform; }

        void Update()
        {
            if (!target) return;
            self.transform.rotation = Quaternion.LookRotation(lookDirection);
        }
        
        Vector3 targetPosition
        {
            get
            {
                if (!x && !y && !z)
                    return target.position;
            
                var result = target.position;
                if (x) result.x = self.position.x;
                if (y) result.y = self.position.y;
                if (x) result.z = self.position.z;
                return result;
            }
        }
    }
}
