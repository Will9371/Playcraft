using UnityEngine;

namespace Playcraft
{
    public class TransformEventListener : GameEventListener
    {
        public TransformEvent TransformResponse;

        public override void OnEventRaised(Transform value)
        { TransformResponse.Invoke(value); }
    }
}