namespace Playcraft
{
    public class IntEventListener : GameEventListener
    {
        public IntEvent IntResponse;

        public override void OnEventRaised(int value)
        { IntResponse.Invoke(value); }
    }
}
