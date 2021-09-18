using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class TimedEvent : MonoBehaviour
    {
        [SerializeField] bool activateOnStart;
        [SerializeField] float time;
        [SerializeField] UnityEvent OnEnd;
        
        void Start() { if (activateOnStart) Begin(); }
        
        public void Begin() { Begin(time); }
        public void Begin(float duration) 
        {
            if (!enabled) return; 
            Invoke(nameof(End), duration); 
        }

        void End() { OnEnd.Invoke(); }
        
        public void Cancel() { CancelInvoke(nameof(End)); }
        
        public void SetTime(float value) { time = value; }        
    }
}
