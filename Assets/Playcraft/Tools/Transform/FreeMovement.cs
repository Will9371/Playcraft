using UnityEngine;

namespace Playcraft
{
    public class FreeMovement : MonoBehaviour
    {
        [SerializeField] float startSpeed = 5f;
        [SerializeField] float minSpeed = 1f;
        [SerializeField] float maxSpeed = 20f;
        float speed;
        
        private void Start()
        {
            speed = startSpeed;
        }
        
        public void Move(Vector3SO data) { Move(data.value); }
        
        public void Move(Vector3 direction)
        {
            var step = direction * speed * Time.deltaTime;
            transform.Translate(step);
        }
        
        public void Accelerate(float value)
        {
            speed += value;
            if (speed < minSpeed) speed = minSpeed;
            if (speed > maxSpeed) speed = maxSpeed;
        }
    }
}
