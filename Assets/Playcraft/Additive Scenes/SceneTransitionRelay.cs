using UnityEngine;

namespace Playcraft
{
    public class SceneTransitionRelay : MonoBehaviour
    {
        [SerializeField] SceneTransitionSOEvent Relay = default;
        public void Input(SceneTransitionSO value) { Relay.Invoke(value); }
    }
}
