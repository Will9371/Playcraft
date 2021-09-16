using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class SlowTime : MonoBehaviour
    {
        [SerializeField] ExtendedCurve conversion;

        public void Input(float value)
        {
            Time.timeScale = conversion.Evaluate(value);
        }
    }
}
