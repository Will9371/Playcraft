using UnityEngine;

namespace Playcraft.Pooling
{
    public class SpawnPrefab : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] GameObject prefab;
        [SerializeField] bool useTransformToPosition = true;
        [SerializeField] Transform location;
        [SerializeField] Vector3 position;
        [SerializeField] GameObjectEvent OutputSpawn;
        #pragma warning restore 0649
        
        public void SetPrefab(GameObject value) { prefab = value; }
        public void SetLocation(Transform value) { location = value; }
        public void SetPosition(Vector3 value) { position = value; }
        
        ObjectPoolMaster pool => ObjectPoolMaster.instance; 
        Vector3 spawnPosition => useTransformToPosition ? location.position : position; 
        
        public void Spawn()
        {
            var spawn = pool.Spawn(prefab, spawnPosition);
            OutputSpawn.Invoke(spawn);
        }
        
        public GameObject GetSpawn()
        {
            var spawn = pool.Spawn(prefab, spawnPosition);
            OutputSpawn.Invoke(spawn);
            return spawn;
        }
    }
}