using UnityEngine;

namespace ZMD
{
    public class LerpCapsuleHeightMono : MonoBehaviour
    {
        [SerializeField] LerpCapsuleHeight process;
        
        void Start() { process.SetCapsuleIfNull(GetComponent<CapsuleCollider>()); }
        public void Input(float value) { process.percent = value; }
        public void SetDirection(bool forward) { process.reverse = !forward; }
    }
}