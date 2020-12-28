using System;
using UnityEngine;
using Playcraft.Navigation;


public class TurnTypeToDirection : MonoBehaviour
{
    [SerializeField] Transform source;
    [SerializeField] bool useLocal;
    [SerializeField] Binding[] bindings;
    [SerializeField] Vector3Event Output;

    public void Input(TurnType value)
    {
        foreach (var binding in bindings)
            if (binding.id == value)
                Output.Invoke(GetDirection(binding));
    }
    
    Vector3 GetDirection(Binding binding)
    {
        if (!useLocal) return binding.direction;
        if (!source) source = transform;
        return source.InverseTransformDirection(binding.direction);
    }
    
    [Serializable] public struct Binding
    {
        public TurnType id;
        public Vector3 direction;
    }
}
