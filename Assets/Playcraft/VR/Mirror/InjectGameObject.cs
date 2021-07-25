using UnityEngine;

// Allows global point of access to scene object from prefab, cross-scene, etc. 
// without reliance on singleton pattern (so possible to have multiple in scene)
namespace Playcraft
{
    public class InjectGameObject : MonoBehaviour
    {
        public GameObjectSO reference;
        void OnValidate() { reference.value = gameObject; }
    }
}