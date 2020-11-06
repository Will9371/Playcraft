using UnityEngine;

namespace Playcraft
{
    public class RaycastSource : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform source;
        [SerializeField] float range = 20f;
        [SerializeField] RaycastHitEvent Hit;
        #pragma warning restore 0649
        
        void Update()
        {
            Ray ray = new Ray(source.position, source.forward);
            Physics.Raycast(ray, out RaycastHit hit, range);
            Hit.Invoke(hit);
        }
    }
}
