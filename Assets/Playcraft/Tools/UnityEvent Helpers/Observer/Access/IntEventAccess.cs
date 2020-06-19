using UnityEngine;

namespace Playcraft
{
    public class IntEventAccess : MonoBehaviour
    {
        [SerializeField] IntGameEvent invoker;
        public void Input(int value) { invoker.Raise(value); }
    }
}
