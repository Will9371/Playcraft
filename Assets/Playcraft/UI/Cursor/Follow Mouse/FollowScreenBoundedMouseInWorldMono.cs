using UnityEngine;

namespace ZMD
{
    public class FollowScreenBoundedMouseInWorldMono : MonoBehaviour
    {
        [SerializeField] FollowScreenBoundedMouseInWorld process;
        void Update() { transform.position = process.Update(); }
        void OnValidate() { process.OnValidate(); }
    }
}
