using UnityEngine;

namespace ZMD
{
    public class MultiConditionMono : MonoBehaviour
    {
        [SerializeField] MultiCondition condition;
        [SerializeField] BoolEvent Output;
        void Awake() { condition.SetObservations(); }
        public void Refresh() { Output.Invoke(condition.IsConditionMet()); }
    }
}