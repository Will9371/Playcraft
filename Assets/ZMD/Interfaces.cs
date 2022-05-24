using UnityEngine;

namespace ZMD
{
    public interface IPercent { float percent { get; set; } }
    public interface ILerpOverTime
    {
        public void SetDuration(float value);
        public bool useCurve { set; }
        public AnimationCurve curve { set; }
        public void FlipAndRun(MonoBehaviour mono);
    }
    
    public interface IRelayBool { void Relay(bool value); }
    public interface ISetString { void Message(string value); }
    public interface ISetSO { void Message(SO value); }
    public interface ISetObject { void Message(GameObject value); }
    
    public interface IGetBool { bool Message(string value); }    
    public interface IGetFloat { float Message(string value); }
}
