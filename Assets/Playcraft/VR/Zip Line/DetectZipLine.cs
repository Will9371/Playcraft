using UnityEngine;

namespace Playcraft.VR
{
    public class DetectZipLine : DetectObject
    {
        [HideInInspector] 
        public ZipLine targetedZipLine;
        
        public bool detected => targetedZipLine != null;
        public bool active => detected && targetedZipLine.active;
                
        protected override void RequestSetComponent(GameObject other)
        {
            var otherZip = other.GetComponent<ZipLine>();
            if (otherZip == null) return;
            targetedZipLine = otherZip;
        }
        
        protected override void RequestUnsetComponent(GameObject other)
        {
            var otherZip = other.GetComponent<ZipLine>();
            if (otherZip == null) return;
            targetedZipLine = null;  
        }
    }
}
