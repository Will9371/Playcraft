using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    public class PlayerBlade : MonoBehaviour
    {
        [SerializeField] UnityEvent Strike;
        [SerializeField] UnityEvent Recover;
        
        void OnTriggerEnter(Collider other)
        {
            var hitAI = other.GetComponent<AIBody>();
            if (hitAI) Strike.Invoke();
            
            var hitRecover = other.GetComponent<PlayerBlade>();
            if (hitRecover) Recover.Invoke();
        }
    }
}
