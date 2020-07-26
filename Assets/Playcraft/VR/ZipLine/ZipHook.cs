using UnityEngine;

namespace Playcraft.VR
{
    public class ZipHook : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] DetectZipLine zipLineDetect;
        [SerializeField] MoveInLine movement;
        #pragma warning restore 0649
        
        Vector3 position => transform.position;

        public void RequestGrabZipLine()
        {
            //Debug.Log("HasTarget = " + zipLineDetect.HasTarget);
            //if (!zipLineDetect.hasTarget)
            //    return;
                
            var line = zipLineDetect.targetedZipLine;      
            movement.StartMoving(position, line.GetFurthestPoint(position), line.speed);
        }
        
        public void ReleaseZipLine()
        {
            movement.InterruptMove();
        }
    }
}
