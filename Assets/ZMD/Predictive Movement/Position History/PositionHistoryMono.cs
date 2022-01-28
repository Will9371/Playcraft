using UnityEngine;

namespace ZMD
{
    public class PositionHistoryMono : MonoBehaviour
    {
        public Transform indicator;
        public PositionHistory process;
        void FixedUpdate() { indicator.position = process.Tick(transform.position); }
    }
}