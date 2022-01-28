using System.Collections;
using UnityEngine;

namespace ZMD.Voxels
{
    public class ShadowSequence : MonoBehaviour
    {
        [SerializeField] ShadowSequenceSO data;
        public void SetData(ShadowSequenceSO value) { data = value; }
        
        [SerializeField] VoxelSeed[] seeds;

        public void Begin() { StartCoroutine(Routine()); }

        IEnumerator Routine()
        {
            foreach (var item in data.values)
            {
                yield return new WaitForSeconds(item.startWaitTime);
                
                foreach (var element in item.values)
                {
                    var seed = seeds[element.seedIndex];
                    seed.Begin(element.perSpawnDelay, element.batchCount);
                }

                yield return new WaitForSeconds(item.GetTotalDuration());
            }
        }
    }
}
