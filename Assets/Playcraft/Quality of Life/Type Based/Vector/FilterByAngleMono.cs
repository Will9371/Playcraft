using System.Collections.Generic;
using UnityEngine;

// Use DrawDebugLinesFromPoint for visualization
namespace Playcraft
{
    public class FilterByAngleMono : MonoBehaviour
    {
        [SerializeField] Transform source;
        [SerializeField] [Range(0f, 360f)] float maxAngle = 45f;
        [SerializeField] ColliderListEvent Output;
        
        ValidateAngleToDot angleDot;
        
        FilterByAngle _process;
        FilterByAngle process
        {
            get
            {
                if (_process == null)
                    _process = new FilterByAngle(source, maxAngle);
                    
                return _process;
            }
        }
                
        void OnValidate() { SetMaxAngle(maxAngle); }
        
        public void SetMaxAngle(float value)
        {
            maxAngle = value;
            process.SetMaxAngle(maxAngle);           
        }
                                
        public void Input(List<Collider> values) { Output.Invoke(process.Input(values)); }
    }
}
