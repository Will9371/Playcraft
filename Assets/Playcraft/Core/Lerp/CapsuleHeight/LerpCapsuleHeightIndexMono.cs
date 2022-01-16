using UnityEngine;

namespace Playcraft
{
    public class LerpCapsuleHeightIndexMono : MonoBehaviour
    {
        [SerializeField] LerpCapsuleHeightIndex process;
        
        void Start()
        {
            process.SetCapsuleIfNull(GetComponent<CapsuleCollider>());
            process.Initialize();
        }
        
        public void SetScaleIndex(int value) { process.SetScaleIndex(value); }

        /// Call continuously to move over time
        public void Input(float percent) { process.Input(percent); }
    }
}
