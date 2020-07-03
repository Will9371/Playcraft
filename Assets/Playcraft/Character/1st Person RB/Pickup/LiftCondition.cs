using UnityEngine;

namespace Playcraft
{
    public class LiftCondition : MonoBehaviour
    {    
        #pragma warning disable 0649
        [SerializeField] ColliderEvent OnGrab;
        [SerializeField] ColliderEvent OnDrop;
        #pragma warning restore 0649
        
        bool keyPressed;    
        Collider touchedObject;

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
