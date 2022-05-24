using UnityEngine;

namespace ZMD
{
    /// Execute a method when a trigger or collision occurs on another object.
    /// Reports both colliders involved in the contact.
    public class ObserveColliderExample : MonoBehaviour
    {
        public ObserveCollider process;

        void OnEnable()
        {
            process.SetEnterTrigger(Enter);
            process.SetExitTrigger(Exit);
        }

        void Enter(Collider self, Collider other)
        {
            Debug.Log($"Enter: {self} {other}");
        }
        
        void Exit(Collider self, Collider other)
        {
            Debug.Log($"Exit: {self} {other}");
        }
    }
}