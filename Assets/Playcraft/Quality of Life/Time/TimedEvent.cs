using UnityEngine;
using UnityEngine.Events;

namespace Playcraft
{
    public class TimedEvent : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] float time;
        [SerializeField] UnityEvent OnEnd;
        #pragma warning restore 0649
        
        public void Begin() { Invoke(nameof(End), time); }
        public void Begin(float duration) { Invoke(nameof(End), duration); }

        private void End() { OnEnd.Invoke(); }
        
        public void Cancel() { CancelInvoke(nameof(End)); }
        
        public void SetTime(float value) { time = value; }        
    }
}
