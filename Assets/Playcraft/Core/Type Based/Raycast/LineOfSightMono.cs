using System.Collections.Generic;
using UnityEngine;

namespace Playcraft
{
    public class LineOfSightMono : MonoBehaviour
    {
        [SerializeField] LineOfSight process;
        [SerializeField] ColliderListEvent Output;
        public void Input(List<Collider> values) { Output.Invoke(process.Input(values)); }
    }
}
