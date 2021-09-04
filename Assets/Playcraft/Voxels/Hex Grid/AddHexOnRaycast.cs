using UnityEngine;
using Playcraft;

public class AddHexOnRaycast : MonoBehaviour
{
    [SerializeField] SO hexTag;

    public void Hit(RaycastHit hit)
    {
        var tags = hit.transform.GetComponent<CustomTags>();
        if (!tags || !tags.HasTag(hexTag)) return;
        
        var addHex = hit.transform.parent.GetComponent<AddHex>();
        if (!addHex || addHex.isReal) return;
        
        addHex.Add();
    }
}
