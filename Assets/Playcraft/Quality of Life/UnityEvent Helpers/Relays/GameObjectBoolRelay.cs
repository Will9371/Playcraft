using UnityEngine;

namespace Playcraft
{
    public class GameObjectBoolRelay : MonoBehaviour
    {
        [SerializeField] GameObjectBoolEvent Output = default;
        public void Input(GameObject source, bool active) { Output.Invoke(source, active); }
    }
}
