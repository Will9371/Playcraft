using System;
using Playcraft;
using UnityEngine;
using UnityEngine.Events;

// MERGE with RespondToComponent & RespondToTouch, rename (simplify)
// Delegate logic out of MonoBehaviour
public class RespondToColliderWithCustomTag : MonoBehaviour
{
    [SerializeField] Binding[] bindings;
    
    public void Input(Collider other)
    {
        var customTags = other.GetComponent<CustomTags>();
        if (!customTags) return;
    
        foreach (var binding in bindings)
            foreach (var validTag in binding.validTags)
                if (customTags.HasTag(validTag))
                    binding.Response.Invoke();
    }

    [Serializable] public struct Binding
    {
        public SO[] validTags;
        public UnityEvent Response;
    }
}
