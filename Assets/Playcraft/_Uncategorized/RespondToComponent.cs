using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class RespondToComponent : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] SO interaction;
        [SerializeField] GameObjectBoolEvent OnEnter;
        [SerializeField] GameObjectBoolEvent OnExit;
        #pragma warning restore 0649

        void OnTriggerEnter(Collider other) { RespondToTouch(OnEnter, other, true); }
        void OnTriggerExit(Collider other) { RespondToTouch(OnExit, other, false); }
        
        void RespondToTouch(GameObjectBoolEvent internalResponse, Collider other, bool activate)
        {
            if (!IsInteractable(other)) return;
            internalResponse.Invoke(other.gameObject, activate);
        }
        
        bool IsInteractable(Collider other)
        {
            var surface = other.GetComponent<CustomTags>();               
            return surface != null && surface.HasTag(interaction);
        }
    }
}
