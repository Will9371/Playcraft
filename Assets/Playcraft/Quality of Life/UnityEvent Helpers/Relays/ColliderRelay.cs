using UnityEngine;

namespace Playcraft
{
    public class ColliderRelay : MonoBehaviour
    {
        [SerializeField] ColliderEvent Output = default;
        public void Input(Collider value) { Output.Invoke(value); }
    }
}
