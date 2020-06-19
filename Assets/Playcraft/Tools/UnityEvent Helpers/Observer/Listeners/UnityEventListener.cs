using UnityEngine.Events;

namespace Playcraft
{
    public class UnityEventListener : GameEventListener
    {
        public UnityEvent Response;

        public override void OnEventRaised()
        { Response.Invoke(); }
    }
}
