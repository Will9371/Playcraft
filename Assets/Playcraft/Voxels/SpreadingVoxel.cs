using UnityEngine;
using Playcraft.Pooling;

namespace Playcraft.Examples.Voxel
{
    public class SpreadingVoxel : MonoBehaviour
    {
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
        GameObject voxelPrefab => prefab.value;
        
        [SerializeField] GameObjectSO prefab;
        [SerializeField] SO voxelTag;
        
        public void Spread()
        {
            if (!gameObject.activeSelf) return;
        
            var position = transform.position + VectorMath.RandomCubeDirection();
            if (!OverlapAtPosition(position))
                spawner.Spawn(voxelPrefab, position);
        }

        bool OverlapAtPosition(Vector3 position)
        {
            var overlap = Physics.OverlapSphere(position, .2f);
            
            foreach (var found in overlap)
            {
                var tags = found.GetComponent<CustomTags>();
                if (!tags) continue;
                
                if (tags.HasTag(voxelTag))
                    return true;
            }
                    
            return false;
        }
    }
}
