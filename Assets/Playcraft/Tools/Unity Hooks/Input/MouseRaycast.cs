using UnityEngine;

namespace Playcraft
{
    public class MouseRaycast : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] RaycastHitEvent Output;
        [SerializeField] LayerMask layerMask;
        [SerializeField] QueryTriggerInteraction triggerInteraction;
        #pragma warning restore 0649

        public void GetHit()
        {
            var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, Mathf.Infinity, layerMask, triggerInteraction))
                Output.Invoke(hit);
        }
    }
}
