using UnityEngine;

namespace Playcraft.Voxels
{
    [CreateAssetMenu(menuName = "Playcraft/Voxel/Seed Spread")]
    public class VoxelSpreadData : ScriptableObject
    {
        public float spawnDelay;
        public int batchCount;
    }
}
