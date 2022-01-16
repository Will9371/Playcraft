using System;
using UnityEngine;

namespace Playcraft.VR
{
    [Serializable]
    public class VRBody
    {
        [SerializeField] Transform eyes;
        [SerializeField] CapsuleCollider capsule;
        
        Vector3 eyePosition => eyes.localPosition;
        
        public void Update()
        {        
            if (!capsule || !eyes) return;
            capsule.height = eyes.localPosition.y;
            capsule.center = new Vector3(eyePosition.x, eyePosition.y/2f, eyePosition.z);
        }
    }
}