using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class MoveInLine : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Rigidbody rb;
        [SerializeField] float defaultSpeed;
        [SerializeField] UnityEvent onMoveStart;
        [SerializeField] UnityEvent onMoveEnd;
        #pragma warning restore 0649

        public void StartMoving(Vector3 end, float speed) { StartMoving(transform.position, end, speed); }
        public void StartMoving(Vector3 end) { StartMoving(transform.position, end, defaultSpeed); }
        public void StartMoving(Vector3 start, Vector3 end) { StartMoving(start, end); }
        public void StartMoving(Vector3 start, Vector3 end, float speed)
        { StartCoroutine(MoveRoutine(start, end, speed)); }
        
        IEnumerator MoveRoutine(Vector3 start, Vector3 end, float speed)
        {
            onMoveStart.Invoke();
            
            var offset = transform.position - start;
            var destination = end + offset;
            Vector3 step;
            
            while (transform.position != destination)
            {
                step = Vector3.MoveTowards(transform.position, destination, Time.deltaTime * speed);
                rb.MovePosition(step);
                yield return null;
            }
            
            EndMove();   
        }
        
        public void InterruptMove()
        {
            StopAllCoroutines();
            EndMove();
        }
        
        void EndMove()
        {
            onMoveEnd.Invoke();
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }
}
