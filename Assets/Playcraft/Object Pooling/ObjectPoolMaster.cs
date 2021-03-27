using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.Pooling
{
    public class ObjectPoolMaster : Singleton<ObjectPoolMaster>
    {
        [SerializeField] ObjectPoolData[] pools = default;
        Dictionary<GameObject, ObjectSpawner> objectPoolDict = new Dictionary<GameObject, ObjectSpawner>();

        void Start()
        {
            foreach (var pool in pools)
            {
                if (pool == null) continue;
                GeneratePool(pool);
            }
        }

        void GeneratePool(ObjectPoolData data)
        {    
            GameObject pool = new GameObject();
            pool.name = data.prefab.name;
            pool.transform.parent = transform;
    
            if (data.prefab == null)
                Debug.LogError("Prefab not set for " + pool.name + " object pool");
                
            if (data.isOnOverlayCanvas) 
            {
                var canvas = pool.AddComponent<Canvas>();
                canvas.renderMode = RenderMode.ScreenSpaceOverlay;
            }

            ObjectSpawner spawner = pool.AddComponent<ObjectSpawner>();
            spawner.Initialize(data);
    
            objectPoolDict.Add(data.prefab, spawner);
        }

        public GameObject Spawn(GameObject obj, Vector3 position)
        {
            ObjectSpawner spawner;
            if (objectPoolDict.TryGetValue(obj, out spawner))
                return spawner.Spawn(position);

            Debug.LogError("Matching prefab not found in object pooling system " + obj.name);
            return null;
        }
                
        public GameObject Spawn(GameObject obj, Vector3 position, Quaternion rotation)
        {
            var newObj = Spawn(obj, position);
            newObj.transform.rotation = rotation;
            return newObj;
        }
        
        public GameObject Spawn(GameObject obj, Vector3 position, Vector3 rotation)
        {
            var newObj = Spawn(obj, position);
            newObj.transform.eulerAngles = rotation;
            return newObj;      
        }
    }
}