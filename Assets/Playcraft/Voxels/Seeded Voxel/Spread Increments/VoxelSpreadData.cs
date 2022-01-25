using UnityEngine;

namespace ZMD.Voxels
{
    [CreateAssetMenu(menuName = "Playcraft/Voxel/Seed Spread")]
    public class VoxelSpreadData : ScriptableObject
    {
        public float spawnDelay;
        public int batchCount;
    }
}
