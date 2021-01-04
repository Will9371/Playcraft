using UnityEngine;

namespace Playcraft
{
    public class ComparePoints : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Transform locationA;
        [SerializeField] Transform locationB;
        #pragma warning restore 0649
        
        Vector3 pointA => locationA.position;
        Vector3 pointB => locationB.position;
                
        public Vector3 GetFurthestPoint(Vector3 accessPoint)
        {        
            return GetFurthestLocation(accessPoint).position;
        }
        
        public Transform GetFurthestLocation(Vector3 accessPoint)
        {
            var distanceA = Vector3.Distance(accessPoint, pointA);
            var distanceB = Vector3.Distance(accessPoint, pointB);
            return distanceA > distanceB ? locationA : locationB;             
        }
        
        public Vector3 GetClosestPoint(Vector3 accessPoint)
        {
            return GetClosestLocation(accessPoint).position;
        }
        
        public Transform GetClosestLocation(Vector3 accessPoint)
        {
            var distanceA = Vector3.Distance(accessPoint, pointA);
            var distanceB = Vector3.Distance(accessPoint, pointB);
            return distanceA < distanceB ? locationA : locationB;             
        }
    }
}
