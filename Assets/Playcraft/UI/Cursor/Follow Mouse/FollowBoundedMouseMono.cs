using UnityEngine;

namespace Playcraft
{
    public class FollowBoundedMouseMono : MonoBehaviour
    {
        [SerializeField] FollowBoundedMouse process;
        void Update() { transform.position = process.Update(); }
    }
}
