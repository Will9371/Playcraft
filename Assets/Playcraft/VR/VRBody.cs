using UnityEngine;

public class VRBody : MonoBehaviour
{
    [SerializeField] Transform eyes;
    
    CapsuleCollider capsule;
    
    Vector3 eyePosition => eyes.localPosition;
    
    void Awake() 
    { 
        capsule = GetComponent<CapsuleCollider>(); 
        
        if (!eyes) Debug.LogError("VRBody.eyes unassigned");
        if (!capsule) Debug.LogError("VRBody component requires a Capsule Collider");
    }

    void Update()
    {        
        if (!capsule || !eyes) return;
        capsule.height = eyes.localPosition.y;
        capsule.center = new Vector3(eyePosition.x, eyePosition.y/2f, eyePosition.z);
    }
}
