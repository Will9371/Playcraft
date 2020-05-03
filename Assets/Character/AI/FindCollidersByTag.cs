using UnityEngine;

public class FindCollidersByTag : MonoBehaviour
{
    [SerializeField] string[] respondToTags;
    [SerializeField] ColliderEvent OnEnter;
    [SerializeField] ColliderEvent OnExit;
    
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
