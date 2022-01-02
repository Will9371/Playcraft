using UnityEngine;

namespace Playcraft
{
    public class FlashColorMono : MonoBehaviour
    {
        [SerializeField] FlashColor process;
        void Start() { process.Start(); }
        public void BeginFlash() { StartCoroutine(process.Flash()); }
        public void BeginFlash(int flashCount) { StartCoroutine(process.Flash(flashCount)); }
    }
}
