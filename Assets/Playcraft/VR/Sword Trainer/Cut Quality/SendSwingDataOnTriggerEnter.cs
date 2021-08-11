using UnityEngine;

namespace Playcraft
{
    public class SendSwingDataOnTriggerEnter : MonoBehaviour
    {
        [SerializeField] SwingDataTracker tracker;

        void OnTriggerEnter(Collider other)
        {
            var target = other.GetComponent<ISwingTarget>();
            target?.SendData(tracker.GetSwingData());
        }
    }
}
