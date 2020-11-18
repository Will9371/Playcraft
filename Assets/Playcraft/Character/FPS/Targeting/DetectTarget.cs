using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.FPS
{
    public class DetectTarget : MonoBehaviour
    {
        public UnityEvent TargetDetected;

        void OnTriggerEnter(Collider other)
        {
            Target target = other.GetComponent<Target>();
            if (target != null) TargetDetected.Invoke();
        }
    }
}
