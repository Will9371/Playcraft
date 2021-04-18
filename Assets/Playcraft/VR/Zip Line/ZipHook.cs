using UnityEngine;

namespace Playcraft.VR
{
    public class ZipHook : MonoBehaviour
    {
        [SerializeField] DetectZipLine zipLineDetect;
        [SerializeField] MoveInLine movement;
        
        Vector3 position => transform.position;

        public void RequestGrabZipLine()
        {
            if (!zipLineDetect.active) return;
            var line = zipLineDetect.targetedZipLine;
            movement.StartMoving(position, line.GetDestination(position), line.speed);
        }
        
        public void ReleaseZipLine()
        {
            movement.InterruptMove();
        }
    }
}
