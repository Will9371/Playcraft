using UnityEngine;

namespace Playcraft.Pooling
{
    public class SpawnPrefab : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] GameObject prefab;
        [SerializeField] Transform location;
        [SerializeField] GameObjectEvent OutputSpawn;
        #pragma warning restore 0649
        
        ObjectPoolMaster pool { get { return ObjectPoolMaster.instance; } }
        Vector3 position { get { return location.position; } }
        
        public void Spawn()
        {
            var spawn = pool.Spawn(prefab, position);
            OutputSpawn.Invoke(spawn);
        }
        
        public void SetPrefab(GameObject value) { prefab = value; }
        public void SetLocation(Transform value) { location = value; }
    }
}
