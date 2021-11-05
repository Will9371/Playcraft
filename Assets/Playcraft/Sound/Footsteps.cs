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
        public void SetMovement(bool value) { isMoving = value; }

        bool isTurning;
        public void SetTurning(bool value) { isTurning = value; }
        
        float nextStepDelay = 0.5f;

        void Start()
        {
            sound = GetComponent<MultiSound>();
            movement = movementContainer.GetComponent<IMoveAndRotate>();
            
            Invoke(nameof(RequestStep), nextStepDelay);
        }

        void RequestStep()
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
            
            Invoke(nameof(RequestStep), nextStepDelay);
        }
    }
}
