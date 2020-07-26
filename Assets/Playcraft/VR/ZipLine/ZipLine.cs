using UnityEngine;

namespace Playcraft.VR
{
    public class ZipLine : MonoBehaviour
    {
        public float speed = 2f;
        public bool charged;
        
        #pragma warning disable 0649
        [SerializeField] Material chargedMaterial;
        [SerializeField] Material unchargedMaterial;
        #pragma warning restore 0649
        
        StretchedWire wire;
        Renderer rend;
        Vector3 pointA => wire.start.position;
        Vector3 pointB => wire.end.position;
        

        private void Awake()
        {
            wire = GetComponent<StretchedWire>();
            rend = GetComponent<Renderer>();
            SetCharge(charged);
        }
        
        public void SetCharge(bool charged)
        {
            this.charged = charged;
            rend.material = charged ? chargedMaterial : unchargedMaterial;
        }
        
        public Vector3 GetFurthestPoint(Vector3 accessPoint)
        {
            if (!charged)
                return accessPoint;
        
            var distanceA = Vector3.Distance(accessPoint, pointA);
            var distanceB = Vector3.Distance(accessPoint, pointB);
            return distanceA > distanceB ? pointA : pointB;    
        }
    }
}
