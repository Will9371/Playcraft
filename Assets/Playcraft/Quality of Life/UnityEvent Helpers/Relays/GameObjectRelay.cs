using UnityEngine;

namespace Playcraft
{
    public class GameObjectRelay : MonoBehaviour, ISetObject
    {
        [SerializeField] GameObjectEvent Output = default;
        public void SetObject(GameObject value) { Output.Invoke(value); }
    }
}
