using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FindClosestColliderMono : MonoBehaviour
    {
        [SerializeField] Transform self;
        [SerializeField] ColliderEvent Output;
        
        FindClosestCollider process;
        
        void Awake() { process = new FindClosestCollider(self); }

        public void Input(List<Collider> others) { Output.Invoke(process.Input(others)); }
    }
}
