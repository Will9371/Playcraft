using UnityEngine;

namespace Playcraft
{
    public class LerpColorMono : MonoBehaviour
    {
        [SerializeField] [Range(0, 1)] float startPercent;
        [SerializeField] LerpColor process;

        void Start() { process.Input(startPercent); }
        public void Input(float percent) { process.Input(percent); }
        public void SetTargetColor(Color value) { process.SetTargetColor(value); }
        public void SetDirection(bool forward) { process.reverse = !forward; }
    }
}