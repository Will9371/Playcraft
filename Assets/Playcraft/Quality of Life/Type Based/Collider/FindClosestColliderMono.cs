using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class FindClosestColliderMono : MonoBehaviour
    {
        [SerializeField] ColliderEvent Output;
        public void Input(List<Collider> others) { Output.Invoke(VectorMath.GetClosest(others, transform.position)); }
    }
}

//FindClosestCollider process;
//{ Output.Invoke(process.Input(others)); }