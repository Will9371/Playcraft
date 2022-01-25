using UnityEngine;

namespace ZMD
{
    public class GameObjectBoolRelay : MonoBehaviour
    {
        [SerializeField] GameObjectBoolEvent Output = default;
        public void Input(GameObject source, bool active) { Output.Invoke(source, active); }
    }
}
