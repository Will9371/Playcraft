using UnityEngine;

namespace Playcraft
{
    public class RaycastSource : MonoBehaviour
    {
        [SerializeField] Transform source;
        [SerializeField] float range = 20f;
        [SerializeField] RaycastHitEvent Hit;
        [SerializeField] bool triggerOnUpdate = true;
        
        void Update() { if (triggerOnUpdate) Trigger(); }
        
        public void Trigger()
        {
            Ray ray = new Ray(source.position, source.forward);
            Physics.Raycast(ray, out RaycastHit hit, range);
            Hit.Invoke(hit);
        }
    }
}
