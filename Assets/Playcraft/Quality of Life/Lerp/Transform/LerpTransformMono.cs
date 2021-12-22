using UnityEngine;

namespace Playcraft
{
    public class LerpTransformMono : MonoBehaviour
    {
        [SerializeField] LerpTransform location;
        
        public void SetStart(Transform value) { location.SetStart(value); }
        public void SetEnd(Transform value) { location.SetEnd(value); }
        
        public void Input(float percent) { location.percent = percent; }
        
        void OnValidate() { location.OnValidate(); }
    }
}