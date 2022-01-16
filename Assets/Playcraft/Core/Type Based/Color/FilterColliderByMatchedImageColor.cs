using UnityEngine;
using UnityEngine.UI;

namespace Playcraft
{
    public class FilterColliderByMatchedImageColor : MonoBehaviour
    {
        [SerializeField] Image image;
        [SerializeField] ColliderEvent Success;
        [SerializeField] ColliderEvent Fail;
        
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