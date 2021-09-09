using System;
using System.Collections;
using UnityEngine;

namespace Playcraft.Examples.GridFollow
{
    [Serializable]
    public class AttackRoutine
    {
        [SerializeField] float waitAfterRotation;
        [SerializeField] float waitInLunge;
        [SerializeField] [Range(0, 1)] float lungeDistanceGrid;
        [SerializeField] LerpPositionOverTime movement;
        
        LerpRotationIndexOverTime rotation;
        GetTargetDirection navigation;
        float gridSize;
        
        float lungeDistance => gridSize * lungeDistanceGrid;
        float rotationWaitTime => rotation.duration + waitAfterRotation;
        
        public void Initialize(LerpRotationIndexOverTime rotation, GetTargetDirection navigation, float gridSize)
        {
            this.rotation = rotation;
            this.navigation = navigation;
            this.gridSize = gridSize;
        }

        public IEnumerator Action()
        {
            var rotationWait = new WaitForSeconds(rotationWaitTime);
            var lungeWait = new WaitForSeconds(movement.duration);
            var attackWait = new WaitForSeconds(waitInLunge);
        
            rotation.TurnToNearest(navigation.targetDirection);
            yield return rotationWait;

            movement.SetStartAtSelf();
            movement.MoveForwardByDistance(lungeDistance);
            yield return lungeWait;
            
            yield return attackWait;
            
            movement.MoveForwardByDistance(-lungeDistance);
            yield return lungeWait;
        }
    }
}
