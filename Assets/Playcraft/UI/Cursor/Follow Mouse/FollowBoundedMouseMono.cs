using UnityEngine;

namespace ZMD
{
    public class FollowBoundedMouseMono : MonoBehaviour
    {
        [SerializeField] FollowBoundedMouse process;
        void Update() { transform.position = process.Update(); }
    }
}
