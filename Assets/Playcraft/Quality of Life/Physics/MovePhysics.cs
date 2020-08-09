using UnityEngine;

namespace Playcraft
{
    public class MovePhysics : MonoBehaviour
    {        
        #pragma warning disable 0649
        [SerializeField] Rigidbody rb;
        [SerializeField] Vector3Event OnMove;
        [SerializeField] float baseSpeed;
        #pragma warning restore 0649
        
        float priorSpeedMultiplier;
        float inputSpeedMultiplier = 1f;
        public void SetSpeedMultiplier(float value) { inputSpeedMultiplier = value; }
        float speedMultiplier => slide ? priorSpeedMultiplier : inputSpeedMultiplier;
        float speed => baseSpeed * speedMultiplier;
        
        Vector3 inputDirection;
        Vector3 priorDirection;
        public void SetDirection(Vector3 value) { inputDirection = value; }
        Vector3 direction => slide ? priorDirection : inputDirection;
        
        bool slide;
        public void SetSlide(bool value) { slide = value; }
           
                        
        private void Awake()
        {            
            if (rb == null)
            {
                rb = GetComponent<Rigidbody>();
                
                if (rb == null)
                    Debug.LogError("Attach or assign a Rigidbody to " + gameObject.name);
            }
        }
        
        Vector3 horizontal;
        Vector3 velocity;
        
        public void Update()
        {
            horizontal = transform.TransformVector(direction * speed);
            velocity = new Vector3(horizontal.x, rb.velocity.y, horizontal.z);
            
            rb.velocity = velocity;
            OnMove.Invoke(velocity);
            
            if (!slide)
            {
                priorSpeedMultiplier = inputSpeedMultiplier;
                priorDirection = inputDirection;
            }
        }
    }
}
