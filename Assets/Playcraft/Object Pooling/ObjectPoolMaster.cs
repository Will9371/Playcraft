using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.Pooling
{
    public class ObjectPoolMaster : Singleton<ObjectPoolMaster>
    {
        [SerializeField] ObjectPoolData[] pools = default;
        private Dictionary<GameObject, ObjectSpawner> objectPoolDict = new Dictionary<GameObject, ObjectSpawner>();

        private void Start()
        {
            foreach (var pool in pools)
            {
                if (pool == null) continue;
                GeneratePool(pool);
            }
        }

        private void GeneratePool(ObjectPoolData data)
        {    
            GameObject pool = new GameObject();
            pool.name = data.prefab.name;
            pool.transform.parent = transform;
    
            if (data.prefab == null)
                Debug.LogError("Prefab not set for " + pool.name + " object pool");
                
            if (data.isOnCanvas) 
            {
                var canvas = pool.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            ObjectSpawner spawner = pool.AddComponent<ObjectSpawner>();
            spawner.Initialize(data);
    
            objectPoolDict.Add(data.prefab, spawner);
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