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
            var line = zipLineDetect.targetedZipLine; 
            if (!line) return;                 
            movement.StartMoving(position, line.GetDestination(position), line.speed);
        }
        
        public void ReleaseZipLine()
        {
            movement.InterruptMove();
        }
    }
}
