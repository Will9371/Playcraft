using System.Collections.Generic;
using Playcraft.Pooling;
using UnityEngine;

namespace Playcraft.Examples.SwordTrainer
{
    public class TargetController : MonoBehaviour
    {
        [SerializeField] GameObject targetPrefab;
        [SerializeField] Vector2 distanceRange;
        [SerializeField] Vector2 heightRange;
        [SerializeField] float moveDuration;
        
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
        List<SwordTarget> targets = new List<SwordTarget>();
        
        int hitCount;

        public void AddTarget()
        {
            var position = RandomStatics.RandomInHollowCylinder(distanceRange, heightRange);
            var targetObj = spawner.Spawn(targetPrefab, position);
            var target = targetObj.GetComponent<SwordTarget>();
            target.Initialize(this);
            targets.Add(target);
        }
            
        public void TargetHit() 
        { 
            hitCount++;
            
            if (hitCount >= targets.Count)
            {
                MoveAllTargets();
                hitCount = 0;
            }
        }
        
        public void RemoveAllTargets()
        {
            for (int i = targets.Count - 1; i >= 0; i--)
                targets[i].Deactivate();
        }
        
        public void MoveAllTargets()
        {
            foreach (var target in targets)
                target.Move(RandomStatics.RandomInHollowCylinder(distanceRange, heightRange), moveDuration);
        }
    }
}
