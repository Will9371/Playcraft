using UnityEngine;
using UnityEngine.Events;

namespace Playcraft.Examples.SwordTrainer
{
    public class PlayerBlade : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] UnityEvent Strike;
        [SerializeField] UnityEvent Recover;
        #pragma warning restore 0649
        
        void OnTriggerEnter(Collider other)
        {
            var hitAI = other.GetComponent<AIBody>();
            if (hitAI) Strike.Invoke();
            
            var hitRecover = other.GetComponent<PlayerBlade>();
            if (hitRecover) Recover.Invoke();
        }
    }
}
