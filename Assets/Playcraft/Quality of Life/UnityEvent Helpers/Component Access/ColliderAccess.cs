using UnityEngine;

namespace Playcraft
{
    public class ColliderAccess : MonoBehaviour
    {
        public void Enable(GameObject other) { Enable(other.GetComponent<Collider>()); }
        public void Enable(Collider other) { other.enabled = true; }
        
        public void Disable(GameObject other) { Disable(other.GetComponent<Collider>()); }
        public void Disable(Collider other) { other.enabled = false; }
    }
}
