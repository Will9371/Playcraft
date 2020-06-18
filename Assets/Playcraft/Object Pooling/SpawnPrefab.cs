using Playcraft.Pooling;
using UnityEngine;

public class SpawnPrefab : MonoBehaviour
{
    [SerializeField] GameObject prefab;
    [SerializeField] Transform location;
    [SerializeField] GameObjectEvent OutputSpawn;
    
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
