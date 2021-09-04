using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    public class AIBlade : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] Vector3SO[] threats;
        [SerializeField] UnityEvent Blocked;
        [SerializeField] UnityEvent Recovered;
        #pragma warning restore 0649
        
                
        public void Input(SwingState value) { Input(value.direction); }
    
        public void Input(Vector3SO otherDestination)
        {
            if (!threats.Contains(otherDestination))
                Recovered.Invoke();
        }
        
        void OnTriggerEnter(Collider other)
        {
            if (IsBlocker(other))
                Blocked.Invoke();
        }
        
        bool IsBlocker(Collider other)
        {
            var otherBlade = other.GetComponent<PlayerBlade>();
            return otherBlade;
        }
    }
}
