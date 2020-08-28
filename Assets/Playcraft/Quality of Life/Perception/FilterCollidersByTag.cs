using UnityEngine;

namespace Playcraft
{
    public class FilterCollidersByTag : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] string[] respondToTags;
        [SerializeField] ColliderEvent OnEnter;
        [SerializeField] ColliderEvent OnStay;
        [SerializeField] ColliderEvent OnExit;
        #pragma warning restore 0649
        
        void OnCollisionEnter(Collision other) { OnTriggerEnter(other.collider); }
        void OnCollisionStay(Collision other) { OnTriggerStay(other.collider); }
        void OnCollisionExit(Collision other) { OnTriggerExit(other.collider); }
        
        void OnTriggerEnter(Collider other) { RespondIfValid(other, OnEnter); }
        void OnTriggerStay(Collider other) { RespondIfValid(other, OnStay); }
        void OnTriggerExit(Collider other) { RespondIfValid(other, OnExit); }
        
        void RespondIfValid(Collider other, ColliderEvent response)
        {
            if (IsValid(other))
                response.Invoke(other);
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
