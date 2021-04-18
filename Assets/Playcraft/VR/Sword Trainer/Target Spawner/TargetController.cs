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
        [SerializeField] Transform player;
        [SerializeField] Transform worldCanvas;
        
        ObjectPoolMaster spawner => ObjectPoolMaster.instance;
        List<SwordTarget> targets = new List<SwordTarget>();
        
        int hitCount;
        
        public void SetPlayer(Transform value) { player = value; }
        
        public void AddTargets(int count)
        {
            for (int i = 0; i < count; i++)
                AddTarget();
        }

        public void AddTarget()
        {
            var position = RandomStatics.RandomInHollowCylinder(distanceRange, heightRange);
            var targetObj = spawner.Spawn(targetPrefab, position);
            var target = targetObj.GetComponent<SwordTarget>();
            target.Initialize(this, player, worldCanvas);
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
            {
                target.isAlive = true;
                target.regenerateFlag = true;
                MoveSingleTarget(target);
            }
        }
        
        public void MoveSingleTarget(SwordTarget target)
        {
            var randomPoint = RandomStatics.RandomInHollowCylinder(distanceRange, heightRange);
            var playerPosition = new Vector3(player.position.x, 0f, player.position.z);
            target.LerpMove(playerPosition + randomPoint, moveDuration);
        }
    }
}
