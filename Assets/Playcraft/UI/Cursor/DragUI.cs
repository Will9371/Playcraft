using UnityEngine;
using UnityEngine.EventSystems;

// For UI elements only, use DragObject for world space objects
namespace Playcraft
{
    public class DragUI : MonoBehaviour, IDragHandler
    {
        [SerializeField] PointerEventData.InputButton button = default;

        public void OnDrag(PointerEventData data)
        {
            if (data.button != button) return;
            transform.Translate(data.delta);
        }
    }
}
