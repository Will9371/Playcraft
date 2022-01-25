using UnityEngine;

namespace ZMD
{
    public class SetStartFocusedSceneMono : MonoBehaviour
    {
        [SerializeField] bool startInScene;
        [SerializeField] ScriptableObject so;
        
        void OnValidate()
        {
            if (!so) return;
            var relay = (IRelayBool)so;
            relay.Relay(startInScene);
        }
    }
}

