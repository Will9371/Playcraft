using System;
using UnityEngine;

namespace Playcraft
{    
    [Serializable] public class MaintainDistance
    {
          public Transform self;
          public Transform target;
          
          [SerializeField] Vector2 desiredDistance;
          [SerializeField] float tolerance;
          [SerializeField] bool disableVerticalMovement;  
          
          float targetDistance => Vector3.Distance(self.position, target.position);
          Vector3 targetDirection => (target.position - self.position).normalized;
          
          Vector3 moveDirection;
          
          public Vector3 Update()
          {
              if (!target) 
                  return Vector3.zero;

              moveDirection = targetDirection * GetMoveTowards();
            
              if (disableVerticalMovement)
                  moveDirection = new Vector3(moveDirection.x, 0f, moveDirection.z);

              return moveDirection;
          }
          
          public void SetDesiredDistance(float value)
          {
              desiredDistance = new Vector2(value - tolerance, value + tolerance);
          }
          
          public float GetMoveTowards()
          {
              if (targetDistance > desiredDistance.y)
                  return 1f;
              if (targetDistance < desiredDistance.x)
                  return -1f;
 
              return 0f;
          }
    }
}
