using UnityEngine;

namespace Playcraft
{
    public interface IMoveAndRotate
    {
        float moveSpeed { get; }
        float turnSpeed { get; }
    }

    public class Footsteps : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float stepsPerMeter = 2f;
        [SerializeField] float stepsPerAngle = 0.02f;
        [SerializeField] float maxStepCheckTime = .5f;
        [SerializeField] GameObject movementContainer;
        #pragma warning restore 0649

        IMoveAndRotate movement;
        MultiSound sound;
        
        bool grounded;
        public void SetGrounded(bool value) { grounded = value; }

        bool isMoving;
        bool isTurning;
        float nextStepDelay = 0.5f;

        private void Start()
        {
            sound = GetComponent<MultiSound>();
            //ground = GetComponent<GroundCheck>();
            movement = movementContainer.GetComponent<IMoveAndRotate>();
            
            Invoke("RequestStep", nextStepDelay);
        }

        public void SetMovement(bool isMoving)
        {
            //Debug.Log("Moving = " + isMoving);
            this.isMoving = isMoving;
        }

        public void SetTurning(bool isTurning)
        {
            //Debug.Log("Turning = " + isTurning);
            this.isTurning = isTurning;
        }

        private void RequestStep()
        {
            if (grounded)
            {
                if (isMoving)
                {
                    nextStepDelay = 1 / (movement.moveSpeed * stepsPerMeter);
                    sound.PlayRandom();
                }
                else if (isTurning)
                {
                    nextStepDelay = 1 / (movement.turnSpeed * stepsPerAngle);
                    sound.PlayRandom();
                }
                else
                    nextStepDelay = 0.5f;
            }

            if (nextStepDelay > maxStepCheckTime)
                nextStepDelay = maxStepCheckTime;
            
            Invoke("RequestStep", nextStepDelay);
        }
    }
}
