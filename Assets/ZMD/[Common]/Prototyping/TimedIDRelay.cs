using UnityEngine;

namespace ZMD
{
    public class TimedIDRelay : MonoBehaviour
    {
        [SerializeField] float time;
        [SerializeField] TagEvent OnEnd;
        
        SO id;
        
        public void Begin(SO value) 
        {
            id = value;
            Invoke(nameof(End), time); 
        }

        private void End() { OnEnd.Invoke(id); }
        public void Cancel() { CancelInvoke(nameof(End)); }
        public void SetTime(float value) { time = value; }
    }
}
