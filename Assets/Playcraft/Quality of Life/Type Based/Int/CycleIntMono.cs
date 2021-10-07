using UnityEngine;

namespace Playcraft
{
    public class CycleIntMono : MonoBehaviour
    {
        [SerializeField] CycleInt process;
        [SerializeField] IntEvent Output;
        
        public void Cycle() { Output.Invoke(process.Cycle()); }
        public void SetValue(int value) { process.SetValue(value); }
        public void SetReverse(bool value) { process.SetReverse(value); }
    }
}
