using UnityEngine;

namespace Playcraft
{
    public class LerpLocationMono : MonoBehaviour
    {
        [SerializeField] LerpLocation location;
        
        public void SetStart(Transform value) { location.SetStart(value); }
        public void SetEnd(Transform value) { location.SetEnd(value); }
        
        public void Input(float percent) { location.percent = percent; }
        
        void OnValidate() { location.Validate(); }
    }
}