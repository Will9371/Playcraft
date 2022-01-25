using System;
using System.Collections.Generic;
using UnityEngine;
using ZMD.Pooling;
using Random = UnityEngine.Random;

namespace ZMD.Voxels
{
    public class SpreadingVoxel : MonoBehaviour
    {
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
        GameObject voxelPrefab => prefab.value;
        
        [SerializeField] GameObjectSO prefab;
        [SerializeField] SO[] blockerTags;
        [SerializeField] SO[] consumeTags;
        [SerializeField] Vector3Array neighborDirections;
        [SerializeField] float overlapCheckDistance = 0.2f;

        // For UnityEvent calls
        public void Spread() { SpreadAndReturn(); }
        
        public SpreadingVoxel SpreadAndReturn()
        {
            if (!gameObject.activeSelf) return null;
        
            var position = GetRandomOpenPosition();
            
            if (OverlapAtPosition(position, blockerTags))
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
                if (!OverlapAtPosition(position, blockerTags))
                    openPositions.Add(position);
            }
        }
        
        public bool HasOpenPosition()
        {
            foreach (var direction in neighborDirections.values)
            {
                // Make extra search optional
                var position = GetAdjacentPosition(direction);
                if (!OverlapAtPosition(position, blockerTags)) return true;

                if (consumeTags.Length == 0) 
                    continue;
                    
                var overlap = Physics.OverlapSphere(position, overlapCheckDistance);
                foreach (var found in overlap)
                {
                    var tags = found.GetComponent<CustomTags>();
                    if (tags == null || !tags.HasAnyTag(consumeTags)) 
                        continue;
                    
                    // * Error-prone, use damage system
                    var root = found.transform.parent.gameObject;
                    root.SetActive(false);
                }
            }
                    
            return false;
        }

        bool OverlapAtPosition(Vector3 position, SO[] tags) { return StaticHelpers.CustomTagNearPosition(position, overlapCheckDistance, tags); }

        public Action<SpreadingVoxel> onDisable;
        
        void OnDisable()
        {
            onDisable?.Invoke(this);
        }
        
        public void Convert(GameObject prefab)
        {
            gameObject.SetActive(false);
            spawner.Spawn(prefab, transform.position);
        }
    }
}
