using UnityEngine;

// Input: external trigger
// Process: check if point on screen is over a valid collider
// Output: RaycastHit (if any)
namespace Playcraft
{
    public class ScreenRaycast : MonoBehaviour
    {
        enum RaycastSource { ScreenCenter, MousePosition }
    
        #pragma warning disable 0649
        [SerializeField] RaycastSource source;
        [SerializeField] float range = 1000;
        [SerializeField] LayerMask layerMask;
        [SerializeField] QueryTriggerInteraction triggerInteraction;
        [SerializeField] RaycastHitEvent Output;
        #pragma warning restore 0649
        
        new Camera camera;
        
        void Start()
        {
            camera = Camera.main;
        }

        public void Trigger()
        {          
            if (Physics.Raycast(GetRay(source), out RaycastHit hit, range, layerMask, triggerInteraction))
                Output.Invoke(hit);
        }
        
        public RaycastHit GetResult()
        {
            Physics.Raycast(GetRay(source), out RaycastHit hit, range, layerMask, triggerInteraction);
            return hit;           
        }
        
        // * Make static
        Ray GetRay(RaycastSource source)
        {
            switch (source)
            {
                case RaycastSource.MousePosition: return camera.ScreenPointToRay(Input.mousePosition);
                case RaycastSource.ScreenCenter: return camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
                default: return camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
            }
        }
    }
}
