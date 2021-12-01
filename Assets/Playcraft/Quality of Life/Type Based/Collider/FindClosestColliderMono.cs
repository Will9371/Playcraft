using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FindClosestColliderMono : MonoBehaviour
    {
        [SerializeField] ColliderEvent Output;
        FindClosestCollider process;
        public void Input(List<Collider> others) { Output.Invoke(process.Input(others)); }
    }
}
