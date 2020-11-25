using UnityEngine;

namespace Playcraft
{
    public class IntRelay : MonoBehaviour
    {
        [SerializeField] IntEvent Output;
        public void Input(int value) { Output.Invoke(value); }
    }
}
