using UnityEngine;

// Allows mirrored object to activate/deactivate independently of original
public class SetMirroredActive : MonoBehaviour
{
    [SerializeField] MirroredObject mirror;
    
    void Start() { if (!mirror) mirror = GetComponent<MirroredObject>(); }
    public void SetActive(bool value) { mirror.mirroredObject.SetActive(value); }
}
