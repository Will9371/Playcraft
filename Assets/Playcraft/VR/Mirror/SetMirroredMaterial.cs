using UnityEngine;

public class SetMirroredMaterial : MonoBehaviour
{
    [SerializeField] MirroredObject mirror;
    [SerializeField] Material material;
    [SerializeField] Renderer rend;

    public void SetOther()
    {
        var other = mirror.mirroredObject.GetComponent<SetMirroredMaterial>();
        other.SetSelf();
    }
    
    public void SetSelf()
    {
        rend.material = material;
    }
}
