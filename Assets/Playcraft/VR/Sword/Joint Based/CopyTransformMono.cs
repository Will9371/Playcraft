using UnityEngine;

public class CopyTransformMono : MonoBehaviour
{
    [SerializeField] CopyTransform process;
    
    void Update() { process.Update(); }
    void FixedUpdate() { process.FixedUpdate(); }
    
    void OnValidate()
    {
        process.self = transform;
        process.rb = GetComponent<Rigidbody>();
    }
}