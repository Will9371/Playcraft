using UnityEngine;

namespace Playcraft
{
    public class Vector3EventListener : GameEventListener
    {
        public Vector3Event Vector3Response;

        public override void OnEventRaised(Vector3 value)
        { Vector3Response.Invoke(value); }
    }
}
