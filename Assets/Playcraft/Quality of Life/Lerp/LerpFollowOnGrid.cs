using System.Collections;
using UnityEngine;
using Playcraft;

public class LerpFollowOnGrid : MonoBehaviour
{
    [SerializeField] LerpPositionOverTime movement;
    [SerializeField] LerpRotationIndexOverTime rotation;
    [SerializeField] GetTargetDirection navigation;
    [SerializeField] float waitAfterRotation;
    [SerializeField] float waitAfterMovement;
    [SerializeField] float gridSize = 1f;
    [SerializeField] float stoppingDistance = 1f;
    
    bool inStoppingDistance => navigation.targetDistance <= stoppingDistance;

    public void Begin() { StartCoroutine(Follow()); }
    public void Stop() { StopAllCoroutines(); }
    
    IEnumerator Follow()
    {
        var rotationDelay = new WaitForSeconds(rotation.duration + waitAfterRotation);
        var moveDelay = new WaitForSeconds(movement.duration + waitAfterMovement);

        while (true)
        {
            rotation.TurnToNearest(navigation.targetDirection);
            yield return rotationDelay;
            
            if (!inStoppingDistance)
            {
                movement.MoveForwardByDistance(gridSize);
                yield return moveDelay;
            }
        }
    }
}
