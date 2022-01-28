using UnityEngine;

namespace ZMD
{
    public class GetClosestMono : MonoBehaviour
    {
        public Transform[] locations;
        public void SetLocations(Transform[] values) { locations = values; }
        
        public Transform GetTransform(Vector3 input) { return IsValidInput(input) ? VectorMath.GetClosest(locations, input) : null; }
        public Vector3 GetPosition(Vector3 input) { return IsValidInput(input) ? VectorMath.GetClosest(locations, input).position : input; }
        
        bool IsValidInput(Vector3 input) { return locations != null && locations.Length > 0; }
    }
}