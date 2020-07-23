using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.VR
{
    public class FilterClimbable : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] UnityEvent OnEnter;
        [SerializeField] UnityEvent OnExit;
        #pragma warning restore 0649

        public void InputEntry(Collider collider)
        {
            if (IsClimbable(collider))
                OnEnter.Invoke();
        }
        
        public void InputExit(Collider collider)
        {
            if (IsClimbable(collider))
                OnExit.Invoke();
        }
        
        private bool IsClimbable(Collider collider)
        {
            var surface = collider.GetComponent<ComponentTags>();
            return surface != null && surface.IsValid(TagID.Climb);
        }
    }
}
