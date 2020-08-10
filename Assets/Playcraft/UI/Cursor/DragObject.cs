// CREDIT: based on Programmer's answer on
// https://stackoverflow.com/questions/41635397/unity3d-ondrag-move-3d-object-doesnt-follow-pointer

using UnityEngine;
using UnityEngine.EventSystems;

namespace Playcraft
{
    public class DragObject : MonoBehaviour, IPointerDownHandler, IDragHandler
    {
        Camera mainCamera;
        float z;
        Vector3 offset;

        void Start()
        {
            mainCamera = Camera.main;
            z = transform.position.z;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            offset = transform.position - mainCamera.ScreenPointToWorldOnPlane(eventData.position, z);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.position = mainCamera.ScreenPointToWorldOnPlane(eventData.position, z) + offset;
        }
    }

    public static class UIExtensions
    {
        public static Vector3 ScreenPointToWorldOnPlane(this Camera camera, Vector3 screenPosition, float z)
        {
            var plane = new Plane(Vector3.forward, new Vector3(0, 0, z));
            var ray = camera.ScreenPointToRay(screenPosition);
            plane.Raycast(ray, out var enterDistance);
            return ray.GetPoint(enterDistance);
        }
    }
}
