using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Playcraft.Pooling;

namespace Playcraft.Voxels
{
    public class VoxelSeed : MonoBehaviour
    {
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
    
        // * Move to VoxelSpreadData
        [SerializeField] float spawnDelay;
        [SerializeField] int batchCount;
        
        [SerializeField] GameObject voxelPrefab;

        List<SpreadingVoxel> voxels = new List<SpreadingVoxel>();
        
        public void Begin(VoxelSpreadData data) { Begin(data.spawnDelay, data.batchCount); }

        public void Begin(float spawnDelay, int batchCount)
        {
            this.spawnDelay = spawnDelay;
            this.batchCount = batchCount;
            Begin();
        }
        
        public void Begin()
        {
            StartCoroutine(nameof(SpawnRoutine));
        }
        
        IEnumerator SpawnRoutine()
        {
            var spawnWait = new WaitForSeconds(spawnDelay);
        
            for (int i = 0; i < batchCount; i++)
            {
                Spawn();
                yield return spawnWait;
            }
        }
        
        void Spawn()
        {
            // If no voxels present, spawn at seed location
            if (voxels.Count == 0)
            {
                var newVoxel = spawner.Spawn(voxelPrefab, transform.position).GetComponent<SpreadingVoxel>();
                AddVoxel(newVoxel);
                return;
            }

            // Otherwise, find a random voxel with an open space and spawn there
            voxels = RandomStatics.ShuffleList(voxels);
            
            foreach (var voxel in voxels)
            {
                if (!voxel.HasOpenPosition()) continue;
                AddVoxel(voxel.SpreadAndReturn());
                break;
            }
        }
        
        void AddVoxel(SpreadingVoxel voxel)
        {
            voxels.Add(voxel);
            voxel.onDisable += VoxelDisabled;
        }

        void VoxelDisabled(SpreadingVoxel voxel)
        {
            voxels.Remove(voxel);
            voxel.onDisable -= VoxelDisabled;
        }
        
        void OnDestroy()
        {
            if (voxels != null)
                foreach (var voxel in voxels)
                    voxel.onDisable -= VoxelDisabled;
        }
    }
}
