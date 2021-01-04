using UnityEngine;

public class SetMaterialByIndex : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] Material[] materials;
    
    public void SetByIndex(int index)
    {
        if (index >= materials.Length) return;
        rend.material = materials[index];
    }
}
