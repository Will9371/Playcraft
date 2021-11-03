using System.Collections;
using UnityEngine;

namespace Playcraft.Pooling
{
    public class PoolingTestSpawner : MonoBehaviour
    {
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
    
        [SerializeField] GameObject prefab;
        [SerializeField] float spawnTime = .2f;
        [SerializeField] float xzRadius;
    
        void Start()
        {
            InvokeRepeating(nameof(Spawn), spawnTime, spawnTime);
        }
        
        void Spawn()
        {
            var xz = Random.insideUnitCircle * xzRadius;
            var position = new Vector3(xz.x, transform.position.y, xz.y);
            spawner.Spawn(prefab, position);
        }
    }
}
