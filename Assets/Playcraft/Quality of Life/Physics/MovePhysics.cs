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
        
        // Only one state active at a time so can be set directly
        float priorSpeedMultiplier;
        float stateSpeedMultiplier = 1f;
        public void SetSpeedMultiplier(float value) { stateSpeedMultiplier = value; }
        float speedMultiplier => slide ? priorSpeedMultiplier : stateSpeedMultiplier;
        float speed => baseSpeed * speedMultiplier;
        
        Vector3 inputDirection;
        Vector3 priorDirection;
        public void SetDirection(Vector3 value) { inputDirection = value; }
        Vector3 direction => slide ? priorDirection : inputDirection;
                        
        void Awake()
        {            
            if (!rb)
            {
                rb = GetComponent<Rigidbody>();
                if (!rb) Debug.LogError("Attach or assign a Rigidbody to " + gameObject.name);
            }
        }
        
        Vector3 horizontal;
        Vector3 velocity;
        
        public void Update()
        {
            horizontal = transform.TransformVector(direction * speed);
            velocity = slide ? rb.velocity : new Vector3(horizontal.x, rb.velocity.y, horizontal.z);
            
            rb.velocity = velocity;
            OnMove.Invoke(velocity);
            
            if (!slide)
            {
                priorSpeedMultiplier = stateSpeedMultiplier;
                priorDirection = inputDirection;
            }
        }
        
        #region Slide
        
        [Tooltip("When any elements in this list are active, character will slide.  " +
                 "To prevent loss of control, remove condition from array.")]
        [SerializeField] PotentialBoolEffects slideCauses;
        
        bool slide => slideCauses.anyActive;
        
        public void ActivateSlide(SO cause) { slideCauses.SetEffectActive(cause, true); }
        public void DeactivateSlide(SO cause) { slideCauses.SetEffectActive(cause, false); }
        
        public void SetSlideForTime(SO cause, float duration) 
        { slideCauses.SetEffectActiveForTime(cause, duration); }
        
        #endregion
    }
}
