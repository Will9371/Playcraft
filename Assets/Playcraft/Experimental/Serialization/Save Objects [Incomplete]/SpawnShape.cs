// INCOMPLETE

using UnityEngine;
using ZMD.Pooling;

// Application-specific interface to SpawnPrefab
namespace ZMD.Saving
{
    public class SpawnShape : MonoBehaviour
    {
        [SerializeField] SpawnPrefab sphereSpawner;
        [SerializeField] SpawnPrefab cubeSpawner;
        
        public GameObject Spawn(ShapeType type)
        {
            switch (type)
            {
                case ShapeType.Sphere: return sphereSpawner.GetSpawn();
                case ShapeType.Cube: return cubeSpawner.GetSpawn();
            }
            
            return null;
        }
    }
}

