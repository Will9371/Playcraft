using UnityEngine;

namespace Playcraft
{
    public class RespondToEventID : MonoBehaviour
    {
        [SerializeField] EventResponder responder;
            
        [SerializeField] bool locked = false;
        public void SetLock(bool value) { locked = value; }

        public void Input(TagSO value)
        {
            if (locked) return;
            var response = responder.GetResponse(value);
            response?.Invoke(); 
        }
    }
}
