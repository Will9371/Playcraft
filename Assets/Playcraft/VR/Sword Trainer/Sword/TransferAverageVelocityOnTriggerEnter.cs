using UnityEngine;

namespace Playcraft
{
    public class TransferAverageVelocityOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] AccumulateVelocity velocity;
        [SerializeField] AccumulateVelocity edge;

        void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<ISwingTarget>();
            if (target == null) return;
            
            target.SetHitSpeed(velocity.averageMagnitude);
            target.SetHitDirection(edge.averageDirection);
            target.SetHitEdge(edge.edgeAlignment);
            target.Refresh();
        }
    }
}
