using UnityEngine;

namespace Playcraft
{
    public class IntRelay : MonoBehaviour
    {
        [SerializeField] IntEvent Output;
        public void Input(int value) { Output.Invoke(value); }
        public void Randomize(int max) { Output.Invoke(Random.Range(0, max)); }
    }
}
