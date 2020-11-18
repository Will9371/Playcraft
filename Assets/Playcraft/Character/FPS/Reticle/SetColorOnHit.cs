using UnityEngine;
using UnityEngine.UI;

namespace Playcraft.FPS
{
    public class SetColorOnHit : MonoBehaviour
    {
        public Image image;
        public Text label;
        public Color inactiveColor;

        public void Input(RaycastHit hit)
        {
            Target target = GetTarget(hit);
            bool isHit = target != null;
            
            image.color = isHit ? target.reticleColor.value : inactiveColor;
            
            label.enabled = isHit;
            if (isHit) label.text = target.label;
        }
        
        Target GetTarget(RaycastHit hit)
        {
            if (hit.collider == null) return null;
            return hit.collider.GetComponent<Target>();
        }
    }
}
