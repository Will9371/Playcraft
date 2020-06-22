using UnityEngine;

namespace Playcraft
{
    public class InteractWithCollider : MonoBehaviour
    {
        public void RequestActivate(Collider value)
        {
            var relay = value.GetComponent<GameObjectBoolRelay>();
            if (!relay) return;            
            relay.Input(gameObject, true);
        }
        
        public void RequestDeactivate(Collider value)
        {
            var relay = value.GetComponent<GameObjectBoolRelay>();
            if (!relay) return;
            relay.Input(gameObject, false);
        }
    }
}
