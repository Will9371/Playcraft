using UnityEngine;
using UnityEngine.Events;

namespace ZMD
{
    public class ValidateEvent : MonoBehaviour
    {
        [SerializeField] bool trigger;
        [SerializeField] UnityEvent Response;
    
        void OnValidate() { CheckTrigger(); }
        
        void CheckTrigger()
        {
            if (!trigger) return;
            Response.Invoke();
            trigger = false;
        }
    }
}
