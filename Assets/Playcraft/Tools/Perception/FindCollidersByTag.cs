using UnityEngine;

namespace Playcraft
{
    public class FindCollidersByTag : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] string[] respondToTags;
        [SerializeField] ColliderEvent OnEnter;
        [SerializeField] ColliderEvent OnExit;
        #pragma warning restore 0649
        
        void OnCollisionEnter(Collision other) { OnTriggerEnter(other.collider); }
        void OnCollisionExit(Collision other) { OnTriggerExit(other.collider); }
        
        void OnTriggerEnter(Collider other)
        {
            if (IsValid(other))
                OnEnter.Invoke(other);
        }
        
        void OnTriggerExit(Collider other)
        {
            if (IsValid(other))
                OnExit.Invoke(other);        
        }
        
        bool IsValid(Collider other)
        {
            foreach (var tag in respondToTags)
                if (other.CompareTag(tag))
                    return true;
                    
            return false;
        }
    }
}
