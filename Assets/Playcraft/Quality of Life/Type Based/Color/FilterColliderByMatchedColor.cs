using UnityEngine;

namespace Playcraft
{
    public class FilterColliderByMatchedColor : MonoBehaviour
    {
        [SerializeField] new Renderer renderer;
        [SerializeField] ColliderEvent Success;
        [SerializeField] ColliderEvent Fail;
        
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
