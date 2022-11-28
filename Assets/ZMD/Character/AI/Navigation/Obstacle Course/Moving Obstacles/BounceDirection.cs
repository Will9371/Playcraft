using UnityEngine;

namespace ZMD
{
    public class BounceDirection : MonoBehaviour
    {
        [SerializeField] bool bounceX, bounceY, bounceZ;
        [SerializeField] Vector3Event Output;
        
        Vector3 direction;
        public void SetDirection(Vector3 value) { direction = value; }
        
        void OnTriggerEnter(Collider other)
        {
            Vector3 bounceDirection = new Vector3();
            bounceDirection.x = bounceX ? -direction.x : direction.x;
            bounceDirection.y = bounceY ? -direction.y : direction.y;
            bounceDirection.z = bounceZ ? -direction.z : direction.z;
            SetDirection(bounceDirection);
            Output.Invoke(direction); 
        }
    }
}
