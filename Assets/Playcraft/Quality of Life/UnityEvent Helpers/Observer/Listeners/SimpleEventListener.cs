using UnityEngine.Events;

namespace Playcraft
{
    public class SimpleEventListener : GameEventListener
    {
        public UnityEvent Response;
        public override void OnEventRaised() { Response.Invoke(); }
    }
}
