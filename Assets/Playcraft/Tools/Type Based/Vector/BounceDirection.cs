using UnityEngine;

namespace Playcraft
{
    public class BounceDirection : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] bool bounceX, bounceY, bounceZ;
        [SerializeField] Vector3Event Output;
        #pragma warning restore 0649
        
        Vector3 direction;
        public void SetDirection(Vector3 value) { direction = value; }
        
        private void OnTriggerEnter(Collider other)
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
