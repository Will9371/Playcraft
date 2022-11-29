using UnityEngine;

namespace ZMD
{
    public class GameObjectRelay : MonoBehaviour, ISetObject
    {
        [SerializeField] GameObjectEvent Output = default;
        public void Message(GameObject value) { Output.Invoke(value); }
    }
}
