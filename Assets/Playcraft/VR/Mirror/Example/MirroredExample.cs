using UnityEngine;

public class MirroredExample : MonoBehaviour
{
    [SerializeField] MirroredObject mirror;
    [SerializeField] Material material;
    
    public void Refresh()
    {
        var mirroredObject = mirror.mirroredObject;
        mirroredObject.GetComponent<Renderer>().material = material;
        
        DestroyImmediate(mirroredObject.GetComponent<MirroredExample>());
        DestroyImmediate(mirroredObject.GetComponent<Collider>());
    }
}
