using UnityEngine;

namespace Playcraft
{
    public class MaintainDistanceMono : MonoBehaviour
    {
        [SerializeField] MaintainDistance process;
        [SerializeField] Vector3Event Output;

        public void SetTarget(Transform value) { process.target = value; }
        
        public void SetDesiredDistance(float value) { process.SetDesiredDistance(value); }
        
        void Start() { if (!process.self) process.self = transform; }
        
        void Update() { Output.Invoke(process.Update()); }
    }
}
