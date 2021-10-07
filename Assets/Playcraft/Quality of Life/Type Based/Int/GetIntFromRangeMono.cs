using UnityEngine;

namespace Playcraft
{
    public class GetIntFromRangeMono : MonoBehaviour
    {
        [SerializeField] GetIntFromRange process;
        [SerializeField] IntEvent Output;
        public void Randomize() { Output.Invoke(process.Randomize()); }
    }
}
