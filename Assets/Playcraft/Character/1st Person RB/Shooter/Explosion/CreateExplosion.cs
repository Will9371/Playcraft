using UnityEngine;

namespace Playcraft.Pooling
{
    public class CreateExplosion : MonoBehaviour
    {
        [SerializeField] GameObject prefab = default;
        
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;

        public void Create()
        {
            var instance = spawner.Spawn(prefab, transform.position);
            var explosion = instance.GetComponent<Explosion>();
            if (explosion) explosion.Explode();
        }
    }
}
