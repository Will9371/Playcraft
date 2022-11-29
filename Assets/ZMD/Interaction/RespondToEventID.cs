using UnityEngine;

namespace ZMD
{
    public class RespondToEventID : MonoBehaviour
    {
        [SerializeField] EventResponder responder;
        [SerializeField] bool locked;
        
        public void SetLock(bool value) { locked = value; }

        public void Input(SO value)
        {
            if (locked) return;
            var response = responder.GetResponse(value);
            response?.Invoke(); 
        }
    }
}
