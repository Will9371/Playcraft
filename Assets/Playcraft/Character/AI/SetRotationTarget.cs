using UnityEngine;

// * Consider refactor: based on hack of BlendDirection
public class SetRotationTarget : MonoBehaviour
{
    [SerializeField] Transform self;
    [SerializeField] BlendDirection rotationController;
    
    Vector3 direction;
    
    public void InputTarget(Collider other)
    {
        if (other == null) 
            rotationController.SetOverride(false, Vector3.zero);
        else
        {
            direction = (other.transform.position - self.position).normalized;
            rotationController.SetOverride(true, direction);            
        }
    }
}
