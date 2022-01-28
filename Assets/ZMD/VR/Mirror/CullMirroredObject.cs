using UnityEngine;

// Remove unwanted child objects and components from mirrored objects
public class CullMirroredObject : MonoBehaviour
{
    [SerializeField] MirroredObject mirror;
    [SerializeField] GameObject[] removeObjects;
    [SerializeField] Component[] removeComponents;

    public void CullOther()
    {
        var other = mirror.mirroredObject.GetComponent<CullMirroredObject>();
        other.CullSelf();
    }
    
    public void CullSelf()
    {
        foreach (var component in removeComponents)
            Destroy(component);
        foreach (var obj in removeObjects)
            Destroy(obj); 
            
        removeObjects = new GameObject[0];
        removeComponents = new Component[0];
    }
}
