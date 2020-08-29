using UnityEngine;

namespace Playcraft
{
    public class FilterColliderByMatchedColor : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] new Renderer renderer;
        [SerializeField] ColliderEvent Success;
        [SerializeField] ColliderEvent Fail;
        #pragma warning restore 0649
        
        void OnEnable()
        {
            if (!renderer) renderer = GetComponent<Renderer>();
        }
        
        public void Input(Collider other)
        {
            var otherRenderer = other.GetComponent<Renderer>();
            if (otherRenderer == null) return;
            var isMatch = renderer.material.color == otherRenderer.material.color;
            var response = isMatch ? Success : Fail;
            response.Invoke(other);
        }
    }
}
