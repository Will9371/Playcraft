using UnityEngine;

namespace Playcraft
{
    public class RotateAxisMono : MonoBehaviour
    {
        [SerializeField] RotateAxis process;
        void OnValidate() { process.ValidateAngle(); }
        public void SetAngle(float value) { process.SetAngle(value); }
    }
}
