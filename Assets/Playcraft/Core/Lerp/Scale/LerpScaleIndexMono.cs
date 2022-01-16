using UnityEngine;

namespace Playcraft
{
    public class LerpScaleIndexMono : MonoBehaviour
    {
        [SerializeField] LerpScaleIndex process;
        
        void Start()
        {
            process.SetSelfIfNull(transform);
            process.Initialize();
        }
        
        public void SetScaleIndex(int value) { process.SetScaleIndex(value); }
        
        public void SetScale(Vector3 value) { process.SetScale(value); }

        /// Call continuously to move over time
        public void Input(float percent) { process.Input(percent); }
    }
}
