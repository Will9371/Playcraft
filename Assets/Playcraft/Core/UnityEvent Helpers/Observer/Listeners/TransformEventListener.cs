using UnityEngine;

namespace ZMD
{
    public class TransformEventListener : GameEventListener
    {
        public TransformEvent TransformResponse;

        public override void OnEventRaised(Transform value)
        { TransformResponse.Invoke(value); }
    }
}