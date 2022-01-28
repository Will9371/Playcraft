using UnityEngine.Events;

namespace ZMD
{
    public class SimpleEventListener : GameEventListener
    {
        public UnityEvent Response;
        public override void OnEventRaised() { Response.Invoke(); }
    }
}
