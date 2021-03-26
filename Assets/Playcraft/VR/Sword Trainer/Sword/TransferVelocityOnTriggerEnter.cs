using UnityEngine;

namespace Playcraft
{
    public class TransferVelocityOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] Rigidbody rb;

        void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<ISwingTarget>();
            target?.SetHitSpeed(rb.velocity.magnitude);
        }
    }
}