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
            
            var swingData = new SwingData(velocity.averageMagnitude, edge.averageDirection, edge.edgeAlignment);
            target.SendData(swingData);
        }
    }
}
