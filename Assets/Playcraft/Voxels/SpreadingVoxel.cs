using System;
using System.Collections.Generic;
using UnityEngine;
using Playcraft.Pooling;
using Random = UnityEngine.Random;

namespace Playcraft.Voxels
{
    public class SpreadingVoxel : MonoBehaviour
    {
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
        GameObject voxelPrefab => prefab.value;
        
        [SerializeField] GameObjectSO prefab;
        [SerializeField] SO voxelTag;
        [SerializeField] Vector3Array neighborDirections;
        [SerializeField] Renderer rend;
        
        // For UnityEvent calls
        public void Spread() { SpreadAndReturn(); }
        
        public SpreadingVoxel SpreadAndReturn()
        {
            if (!gameObject.activeSelf) return null;
        
            var position = GetRandomOpenPosition();
            
            if (OverlapAtPosition(position))
                return null;
            
            return spawner.Spawn(voxelPrefab, position).GetComponent<SpreadingVoxel>();
        }
        
        Vector3 GetRandomOpenPosition()
        {
            RefreshOpenPositions();
            return openPositions.Count == 0 ? 
                Vector3.zero : 
                openPositions[Random.Range(0, openPositions.Count)];
        }
        
        Vector3 GetAdjacentPosition(Vector3 direction)
        {
            var offset = Vector3.Scale(direction, transform.localScale);
            return transform.position + offset;
        }

        List<Vector3> openPositions = new List<Vector3>();
        
        void RefreshOpenPositions()
        {
            openPositions.Clear();
            
            foreach (var direction in neighborDirections.values)
            {
                var position = GetAdjacentPosition(direction);
                if (!OverlapAtPosition(position))
                    openPositions.Add(position);
            }
        }
        
        public bool HasOpenPosition()
        {
            foreach (var direction in neighborDirections.values)
                if (!OverlapAtPosition(GetAdjacentPosition(direction)))
                    return true;
                    
            return false;
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
        
        public void SetMaterial(Material material) { rend.material = material; }
        
        public Action<SpreadingVoxel> onDisable;
        
        void OnDisable()
        {
            onDisable?.Invoke(this);
        }
    }
}
