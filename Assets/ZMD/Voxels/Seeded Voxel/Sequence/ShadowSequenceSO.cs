using System;
using UnityEngine;

namespace ZMD.Voxels
{
    [CreateAssetMenu(menuName = "ZMD/Voxel/Shadow Sequence")]
    public class ShadowSequenceSO : ScriptableObject
    {
        public ShadowArraySpreadData[] values;
    }

    [Serializable]
    public class ShadowArraySpreadData
    {
        public float startWaitTime;
        public ShadowSpreadData[] values;
        
        public float GetTotalDuration()
        {
            float longest = 0f;
            
            foreach (var value in values)
                if (value.fullDuration > longest)
                    longest = value.fullDuration;
                    
            return longest;
        }
    }

    [Serializable]
    public struct ShadowSpreadData
    {
        public int seedIndex;
        public float perSpawnDelay;
        public int batchCount;
        
        public float fullDuration => perSpawnDelay * batchCount;
    } 
}
