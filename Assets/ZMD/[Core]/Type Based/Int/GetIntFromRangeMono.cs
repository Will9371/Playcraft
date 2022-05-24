using UnityEngine;

namespace ZMD
{
    public class GetIntFromRangeMono : MonoBehaviour
    {
        [SerializeField] GetIntFromRange process;
        [SerializeField] IntEvent Output;
        public void Randomize() { Output.Invoke(process.Randomize()); }
    }
}
