using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class MouseInput : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float minClicktime;

        [SerializeField] Vector2Event OnMouseDrag;
        [SerializeField] float scrollSensitivity = 1f;
        [SerializeField] FloatEvent OnMouseScroll;
        [SerializeField] float xDragSensitivity = 1f;
        [SerializeField] FloatEvent OnMouseDragX;
        [SerializeField] float yDragSensitivity = 1f;
        [SerializeField] FloatEvent OnMouseDragY;
        [SerializeField] UnityEvent OnLeftClick, OnRightClick;
        #pragma warning restore 0649

        // Cached
        float lastMouseY, mouseYDelta;
        float lastMouseX, mouseXDelta;
        float leftClickTime, rightClickTime;

        public bool locked;
        public bool scrollLocked;
        public void SetLock(bool locked) { this.locked = locked; }

        private void Update()
        {
            if (locked)
                return;

            GetLeftMouseDrag();
            GetMouseScroll();
            GetLeftClick();
            GetRightClick();
        }

        private void GetLeftClick()
        {
            if (Input.GetMouseButtonDown(0))
                leftClickTime = Time.time;
            else if (Input.GetMouseButtonUp(0))
                if (Time.time - leftClickTime < minClicktime)
                    OnLeftClick.Invoke();
        }

        private void GetRightClick()
        {
            if (Input.GetMouseButtonDown(1))
                rightClickTime = Time.time;
            else if (Input.GetMouseButtonUp(1))
                if (Time.time - rightClickTime < minClicktime)
                    OnRightClick.Invoke();
        }

        private void GetLeftMouseDrag()
        {
            // Prevent jump on next mouse click
            if (Input.GetMouseButtonDown(0))
            {
                lastMouseX = Input.mousePosition.x;
                lastMouseY = Input.mousePosition.y;
            }
            // Get drag since last frame
            else if (Input.GetMouseButton(0))
            {
                mouseXDelta = Input.mousePosition.x - lastMouseX;
                mouseYDelta = Input.mousePosition.y - lastMouseY;

                lastMouseX = Input.mousePosition.x;
                lastMouseY = Input.mousePosition.y;
            }
            // Prevent ongoing drift
            else if (Input.GetMouseButtonUp(0))
            {
                mouseXDelta = 0;
                mouseYDelta = 0;
            }

            OnMouseDrag.Invoke(new Vector2(mouseXDelta * xDragSensitivity, mouseYDelta * yDragSensitivity));
            OnMouseDragX.Invoke(mouseXDelta * xDragSensitivity);
            OnMouseDragY.Invoke(mouseYDelta * yDragSensitivity);
        }

        private void GetMouseScroll()
        {
            if (Input.mouseScrollDelta.y != 0)
                OnMouseScroll.Invoke(Input.mouseScrollDelta.y * scrollSensitivity);
        }
    }
}
