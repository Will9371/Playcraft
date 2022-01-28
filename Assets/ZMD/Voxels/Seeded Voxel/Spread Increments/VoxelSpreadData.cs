using UnityEngine;

namespace ZMD.Voxels
{
    [CreateAssetMenu(menuName = "ZMD/Voxel/Seed Spread")]
    public class VoxelSpreadData : ScriptableObject
    {
        public float spawnDelay;
        public int batchCount;
    }
}
