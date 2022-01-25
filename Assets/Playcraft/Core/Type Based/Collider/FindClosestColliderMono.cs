using System.Collections.Generic;
using UnityEngine;

namespace ZMD
{
    public class FindClosestColliderMono : MonoBehaviour
    {
        [SerializeField] ColliderEvent Output;
        public void Input(List<Collider> others) { Output.Invoke(VectorMath.GetClosest(others, transform.position)); }
    }
}

//FindClosestCollider process;
//{ Output.Invoke(process.Input(others)); }