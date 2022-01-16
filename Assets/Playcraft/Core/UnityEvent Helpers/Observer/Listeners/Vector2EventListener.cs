using UnityEngine;

namespace Playcraft
{
    public class Vector2EventListener : GameEventListener
    {
        public Vector2Event Vector2Response;

        public override void OnEventRaised(Vector2 value)
        { Vector2Response.Invoke(value); }
    }
}
