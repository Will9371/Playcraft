using UnityEngine;

namespace Playcraft
{
    public class BoolRelayAccess : MonoBehaviour
    {
        [SerializeField] bool defaultValue = default;
        
        public void Input(RaycastHit hit) { Input(hit, defaultValue); }
        public void Input(RaycastHit hit, bool value)
        {
            var relay = hit.collider.GetComponent<BoolRelay>();
            if (relay != null) relay.Input(value);
        }
    }
}
