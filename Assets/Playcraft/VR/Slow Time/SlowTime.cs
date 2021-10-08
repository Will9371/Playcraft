using UnityEngine;

namespace Playcraft.VR
{
    public class SlowTime : MonoBehaviour
    {
        [SerializeField] ExtendedCurve conversion;
        public void Input(float value) { Time.timeScale = conversion.Evaluate(value); }
        public void SetDefault() { Input(1f); }
    }
}