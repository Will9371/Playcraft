using System;
using System.Collections;
using UnityEngine;

namespace ZMD.Examples.GridFollow
{
    [Serializable]
    public class FollowRoutine 
    {
        [SerializeField] float waitAfterRotation;
        [SerializeField] float waitAfterMovement;
        [SerializeField] LerpPositionOverTime movement;
        
        LerpRotationIndexOverTime rotation;
        GetTargetDirection navigation;
        float gridSize;
        
        float rotationWaitTime => rotation.duration + waitAfterRotation;
        float moveWaitTime => movement.duration + waitAfterMovement;
        
        public void Initialize(LerpRotationIndexOverTime rotation, GetTargetDirection navigation, float gridSize)
        {
            this.rotation = rotation;
            this.navigation = navigation;
            this.gridSize = gridSize;
        }

        public IEnumerator Action()
        {
            rotation.TurnToNearest(navigation.targetDirection);
            yield return new WaitForSeconds(rotationWaitTime);

            movement.MoveForwardByDistance(gridSize);
            yield return new WaitForSeconds(moveWaitTime);
        }
    }
}