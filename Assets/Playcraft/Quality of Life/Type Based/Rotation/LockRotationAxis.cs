using UnityEngine;
using Playcraft;

public class LockRotationAxis : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] Transform target;
    [SerializeField] Axis axis;
    
    Quaternion rotation;

    void FixedUpdate()
    {
        if (!self || !target) return;
        
        rotation = self.localRotation;
    
        switch (axis)
        {
            case Axis.X: self.localRotation = new Quaternion(target.localRotation.x, rotation.y, rotation.z, rotation.w); break;
            case Axis.Y: self.localRotation = new Quaternion(rotation.x, target.localRotation.y, rotation.z, rotation.w); break;
            case Axis.Z: self.localRotation = new Quaternion(rotation.x, rotation.y, target.localRotation.z, rotation.w); break;
        }
    }
}
