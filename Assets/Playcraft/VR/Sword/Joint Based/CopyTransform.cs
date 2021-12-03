// CREDIT: Fraser Hill

// DEPRECATE: causes swords to move through each other 
// (more noticeable when rotation set in this script as well as position)
// Replace with script that moves object gradually, preferably based on physics forces.
// Consider 3-tier abstraction: intention (instant) -> hand (fast physics follow) -> sword (configurable joint)
// Delete this script if swords are the only place it is used

using UnityEngine;

public class CopyTransform : MonoBehaviour
{
    [SerializeField] Transform target;
    [SerializeField] bool useRigidbody;
    [SerializeField] bool position;
    [SerializeField] bool rotation;
    [SerializeField] bool scale;
    
    Rigidbody rb;
    
    void Awake()
    {
        if (useRigidbody && rb == null)
        {
            rb = GetComponent<Rigidbody>();
            if (rb == null)
                Debug.LogError(gameObject.name + " " +
                "CopyTransform set to use rigidbody but no rigidbody attached" );
        }
    }

    void Update()
    {
        if (scale) transform.localScale = target.localScale;
        
        if (useRigidbody) return;
        
        if (position) transform.position = target.position;
        if (rotation) transform.rotation = target.rotation;
    }

    void FixedUpdate()
    {
        if (!useRigidbody || !rb) return;

        if (position) rb.MovePosition(target.position);
        if (rotation) rb.MoveRotation(target.rotation);
    }
}
