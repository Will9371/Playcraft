using UnityEngine;

namespace Playcraft
{
    public class FollowInstant : MonoBehaviour
    {
        public Transform target;

        void LateUpdate()
        {
            if (!target) return;
            transform.position = target.position;
            transform.rotation = target.rotation;
        }
    }
}
