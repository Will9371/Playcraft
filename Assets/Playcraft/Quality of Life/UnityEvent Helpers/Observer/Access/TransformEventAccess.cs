using UnityEngine;

namespace Playcraft
{
    public class TransformEventAccess : MonoBehaviour
    {
        [SerializeField] TransformGameEvent invoker = default;
        public void Input(Transform value) { invoker.Raise(value); }
    }
}