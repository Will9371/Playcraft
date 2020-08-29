using UnityEngine;
using UnityEngine.UI;

namespace Playcraft
{
    public class FilterColliderByMatchedImageColor : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Image image;
        [SerializeField] ColliderEvent Success;
        [SerializeField] ColliderEvent Fail;
        #pragma warning restore 0649
        
        void OnEnable()
        {
            if (!image) image = GetComponent<Image>();
        }
        
        public void Input(Collider other)
        {
            var otherImage = other.GetComponent<Image>();
            if (otherImage == null) return;
            var isMatch = image.color == otherImage.color;
            var response = isMatch ? Success : Fail;
            response.Invoke(other);
        }
    }
}