using System.Collections.Generic;
using UnityEngine;

// Use DrawDebugLinesFromPoint for visualization
namespace Playcraft
{
    public class FilterByAngleMono : MonoBehaviour
    {
        [SerializeField] ColliderListEvent Output;
        
        ValidateAngleToDot angleDot;
        FilterByAngle process;

        void OnValidate() { process.Validate(); }
        public void SetMaxAngle(float value) { process.SetMaxAngle(value); }
        public void Input(List<Collider> values) { Output.Invoke(process.Input(values)); }
    }
}
