using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class OutputSwingData : MonoBehaviour
    {
        [SerializeField] SwingDataTracker swingDataTracker;
        [SerializeField] CutQualityCalculator calculator;
        [SerializeField] FloatEvent output;

        void FixedUpdate()
        {
            output.Invoke(calculator.GetCutQuality(swingDataTracker.GetSwingData()));
        }
    }
}
