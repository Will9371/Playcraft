// CREDIT: based on Programmer's answer on
// https://stackoverflow.com/questions/41635397/unity3d-ondrag-move-3d-object-doesnt-follow-pointer

using UnityEngine;
using UnityEngine.EventSystems;

namespace Playcraft
{
    public class DragObject : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        #pragma warning disable 0649
        [SerializeField] new Camera camera;
        [SerializeField] PointerEventData.InputButton[] buttons;
        [SerializeField] bool useWorldSpace;
        #pragma warning restore 0649
    
        float z;
        Vector3 offset;

        void Start()
        {
            if (!camera) camera = Camera.main;
            z = transform.position.z;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (!isValidButton(eventData)) return;
            offset = transform.position - GetPosition(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!isValidButton(eventData)) return;
            transform.position = GetPosition(eventData) + offset;
        }
        
        bool isValidButton(PointerEventData eventData)
        {
            foreach (var button in buttons)
                if (button == eventData.button)
                    return true;
                    
            return false;
        }
        
        Vector3 GetPosition(PointerEventData eventData)
        {
            return useWorldSpace ? 
                camera.ScreenPointToWorld(eventData.position, z) : 
                new Vector3(eventData.position.x, eventData.position.y, z);
        }
    }

    public static class UIExtensions
    {
        public static Vector3 ScreenPointToWorld(this Camera camera, Vector3 screenPosition, float z)
        {
            var plane = new Plane(Vector3.forward, new Vector3(0, 0, z));
            var ray = camera.ScreenPointToRay(screenPosition);
            plane.Raycast(ray, out var enterDistance);
            return ray.GetPoint(enterDistance);
        }
    }
}