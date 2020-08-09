using UnityEngine;

namespace Playcraft
{
    public class MovePositionRB : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Rigidbody physicsObject;
        [SerializeField] Transform movingObject;
        #pragma warning restore 0649

        public void MoveStep(Vector3 input)
        {
            physicsObject.MovePosition(movingObject.position + input * Time.deltaTime);
        }
        
        public void SetVelocity(Vector3 input)
        {
            physicsObject.AddForce(input - physicsObject.velocity, ForceMode.VelocityChange);
        }
    }
}
