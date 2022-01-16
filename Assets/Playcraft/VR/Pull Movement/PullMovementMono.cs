using UnityEngine;

using System.Collections.Generic;

namespace Playcraft.VR
{
    public class PullMovementMono : MonoBehaviour
    {
        [SerializeField] PullMovement process;
        void Update() { process.Update(); }
        public void MoveThisFrame() { process.moveThisFrame = true; }
    }
}