namespace ZMD
{
    public class FloatEventListener : GameEventListener
    {
        public FloatEvent FloatResponse;
        public override void OnEventRaised(float value) { FloatResponse.Invoke(value); }
    }
}
