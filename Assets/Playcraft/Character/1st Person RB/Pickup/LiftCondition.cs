using UnityEngine;

namespace Playcraft
{
    public class LiftCondition : MonoBehaviour
    {    
        [SerializeField] ColliderEvent OnGrab;
        [SerializeField] ColliderEvent OnDrop;
        
        bool keyPressed;    
        Collider touchedObject;

        // For example: access pickup directly rather than working through UnityEvents
        public void ReceiveInput(bool isPressed)
        {
            keyPressed = isPressed;
        
            if (isPressed && touchedObject != null)
                OnGrab.Invoke(touchedObject);
            else if (!isPressed && touchedObject != null)
                OnDrop.Invoke(touchedObject);
        }
        
        public void ReceiveTouch(Collider other)
        {
            touchedObject = other;
        }
        
        public void ClearTouch()
        {
            if (touchedObject != null && keyPressed)
                OnDrop.Invoke(touchedObject);
            
            touchedObject = null;
        }
    }
}
