using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class MoveInLine : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float stoppingDistance = 0.25f;
        [SerializeField] UnityEvent onMoveStart;
        [SerializeField] UnityEvent onMoveEnd;
        #pragma warning restore 0649
        
        Rigidbody rb;

        private void Awake()
        {
            rb = GetComponent<Rigidbody>();
        }
        
        public void StartMoving(Vector3 start, Vector3 end, float speed)
        {
            StartCoroutine(MoveRoutine(start, end, speed));
        }
        
        IEnumerator MoveRoutine(Vector3 start, Vector3 end, float speed)
        {
            onMoveStart.Invoke();
            
            var wait = new WaitForEndOfFrame();
            var direction = (end - start).normalized;
            var offset = transform.position - start;
            var destination = end + offset;
            Vector3 step;
            
            while (Vector3.Distance(transform.position, destination) > stoppingDistance)
            {
                step =  Time.deltaTime * speed * direction;
                rb.MovePosition(transform.position + step);
                yield return wait;
            }
            
            EndMove();   
        }
        
        public void InterruptMove()
        {
            StopAllCoroutines();
            EndMove();
        }
        
        private void EndMove()
        {
            onMoveEnd.Invoke();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
