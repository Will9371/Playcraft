using UnityEngine;

namespace Playcraft
{
    public class RespondToMessage : MonoBehaviour, IMessage
    {
        [SerializeField] new bool enabled = true;
        [SerializeField] EventResponder responses = default;
        
        public void Message(SO value) 
        { 
            if (!enabled) return; 
            responses.GetResponse(value)?.Invoke(); 
        }
        
        public void Enable(bool value) { enabled = value; }
    }
}
