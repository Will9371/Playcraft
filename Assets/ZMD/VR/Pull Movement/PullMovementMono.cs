using UnityEngine;

using System.Collections.Generic;

namespace ZMD.VR
{
    public class PullMovementMono : MonoBehaviour
    {
        [SerializeField] PullMovement process;
        void Update() { process.Update(); }
        public void MoveThisFrame() { process.moveThisFrame = true; }
    }
}