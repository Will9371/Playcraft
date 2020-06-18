using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    namespace Pooling
    {
        public class ObjectPoolMaster : Singleton<ObjectPoolMaster>
        {
            [SerializeField] ObjectPoolData[] pools;
            private Dictionary<GameObject, ObjectSpawner> objectPoolDict = new Dictionary<GameObject, ObjectSpawner>();
    
            private void Start()
            {
                foreach (var pool in pools)
                {
                    if (pool == null)
                        continue;

                    GeneratePool(pool.prefab, pool.startSize);
                }
            }

            private void GeneratePool(GameObject template, int startingPoolSize)
            {    
                GameObject pool = new GameObject();
                pool.name = template.name;
                pool.transform.parent = transform;
        
                if (template == null)
                    Debug.LogError("Prefab not set for " + pool.name + " object pool");

                ObjectSpawner spawner = pool.AddComponent<ObjectSpawner>();
                spawner.Initialize(template, startingPoolSize);
        
                objectPoolDict.Add(template, spawner);
            }

            public GameObject Spawn(GameObject prefabToSpawn, Vector3 spawnLocation)
            {
                ObjectSpawner spawner;
                if (objectPoolDict.TryGetValue(prefabToSpawn, out spawner))
                {
                    //Debug.Log("Pool spawn method reached " + spawner.name);
                    return spawner.Spawn(spawnLocation);
                }

                Debug.LogError("Matching prefab not found in object pooling system " + prefabToSpawn.name);
                return null;
            }
        }
    }
}
