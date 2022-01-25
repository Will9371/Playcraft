using UnityEngine;

namespace ZMD
{
    public class GameObjectRelay : MonoBehaviour, ISetObject
    {
        [SerializeField] GameObjectEvent Output = default;
        public void SetObject(GameObject value) { Output.Invoke(value); }
    }
}
