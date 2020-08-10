using UnityEngine;

// DEPRECATED: use PhysicsRaycaster on camera, RespondToMouse on interactables
// (Potential use: object that needs to respond to multiple sources, including mouse)

// Input: external trigger, optional event tag
// Process: check if mouse is over a valid collider
// Output: RaycastHit (if any), relay event tag to hit if it has and attached RespondToEventID
namespace Playcraft
{
    public class MouseRaycast : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] RaycastHitEvent Output;
        [SerializeField] LayerMask layerMask;
        [SerializeField] QueryTriggerInteraction triggerInteraction;
        #pragma warning restore 0649

        public void GetHit(TagSO id = null)
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (!Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, triggerInteraction)) return;
            Output.Invoke(hit);
                
            if (id == null) return;
            var relay = hit.collider.GetComponent<RespondToEventID>();
            if (relay) relay.Input(id);
        }
    }
}
