namespace Playcraft
{
    public class FloatArrayEventListener : GameEventListener
    {
        public FloatArrayEvent FloatArrayResponse;

        public override void OnEventRaised(float[] value)
        { FloatArrayResponse.Invoke(value); }
    }
}