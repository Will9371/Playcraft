using UnityEngine;

namespace ZMD
{
    public class RotateAxisMono : MonoBehaviour
    {
        [SerializeField] RotateAxis process;
        void OnValidate() { process.ValidateAngle(); }
        public void SetAngle(float value) { process.SetAngle(value); }
    }
}
