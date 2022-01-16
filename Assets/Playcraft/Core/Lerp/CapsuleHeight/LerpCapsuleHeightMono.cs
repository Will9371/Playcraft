using UnityEngine;

namespace Playcraft
{
    public class LerpCapsuleHeightMono : MonoBehaviour
    {
        [SerializeField] LerpCapsuleHeight process;
        
        void Start() { process.SetCapsuleIfNull(GetComponent<CapsuleCollider>()); }
        public void Input(float percent) { process.Input(percent); }
        public void SetDirection(bool forward) { process.reverse = !forward; }
    }
}
