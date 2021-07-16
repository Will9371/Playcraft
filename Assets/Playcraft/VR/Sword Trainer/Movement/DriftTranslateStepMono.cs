using UnityEngine;

namespace Playcraft
{
    public class DriftTranslateStepMono : MonoBehaviour
    {
        [SerializeField] DriftTranslateStep process;
        void Awake() { if (!process.self) process.self = transform; }
        void Update() { process.Update(); }
        public void AddMovement(Vector3 value) { process.AddMovement(value); }
    }
}
