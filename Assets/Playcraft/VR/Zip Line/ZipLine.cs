using UnityEngine;

namespace Playcraft.VR
{
    public class ZipLine : MonoBehaviour
    {
        [SerializeField] ComparePoints points;
        [SerializeField] SplitBool activeState;
        public float speed = 2f;
        [SerializeField] float stoppingDistance;
        
        public bool active => activeState.state;
        
        public Vector3 GetDestination(Vector3 position)
        {
            return active ? GetFurthestPoint(position) : position;
        }
        
        Vector3 GetFurthestPoint(Vector3 position)
        {
            var furthest = points.GetFurthestPoint(position);
            var closest = points.GetClosestPoint(position);
            var cutoffDirection = (closest - furthest).normalized;
            return furthest + cutoffDirection * stoppingDistance;
        }
    }
}
