using System;
using UnityEngine;

namespace ZMD.VR
{
    [Serializable]
    public class VRBody
    {
        [SerializeField] Transform eyes;
        public CapsuleCollider body;
        public SphereCollider feet;
        
        Vector3 eyePosition => eyes.localPosition;
        
        public void Update()
        {
            if (!eyes) return;
            
            if (body)
            {
                body.height = eyes.localPosition.y;
                body.center = new Vector3(eyePosition.x, eyePosition.y/2f, eyePosition.z);
            }
            if (feet)
                feet.center = new Vector3(eyePosition.x, feet.radius, eyePosition.z);
        }
    }
}