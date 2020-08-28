using System.Collections.Generic;
using UnityEngine;

namespace Playcraft.Pooling
{
    public class ObjectSpawner : MonoBehaviour 
    {
        List<GameObject> pooledObjects = new List<GameObject>();
        
        ObjectPoolData data;
        GameObject pooledObject => data.prefab; 		
        int currentPoolSize;
        int index; 	

        public void Initialize(ObjectPoolData data)
        {
            this.data = data;
            currentPoolSize = data.startSize;

            for (int i = 0; i < currentPoolSize; i++)
                Add(false);
        }

        public GameObject Spawn(Vector3 spawnLoc)
        {
            GameObject selected = GetPooledObject();

            if (data.isOnCanvas)
            {
                var rect = selected.transform as RectTransform;
                if (rect != null) rect.anchoredPosition3D = spawnLoc;
            }
            else
                selected.transform.position = spawnLoc;
            
            selected.transform.SetParent(gameObject.transform);  
            selected.SetActive(true);

            Rigidbody rb = selected.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
            }

            return selected;
        }

        private GameObject GetPooledObject()
        {
            bool poolEmpty = true;
            
            for(index = 0; index < currentPoolSize; index++)
            {
                if(pooledObjects[index] == null)
                {
                    Debug.LogError("WARNING: object " + index + " in " + gameObject.name + " cannot be found!");
                    return null;
                }

                if (!pooledObjects[index].activeInHierarchy)
                {
                    poolEmpty = false;
                    break;
                }
            }

            if (poolEmpty)
            {
                Add(true);
                currentPoolSize++;
                index = currentPoolSize - 1;
            }

            return pooledObjects[index];
        }

        private void Add(bool isActive)
        {
            GameObject obj = Instantiate(pooledObject);
            obj.transform.SetParent(gameObject.transform);
            obj.SetActive(isActive);
            pooledObjects.Add(obj);
        }
    }
}