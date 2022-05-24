using System;
using UnityEngine;

[Serializable]
public class PropertyBlockInterface
{
    [SerializeField] Renderer rend;
    [SerializeField] string property = "_MainTex_ST";

    MaterialPropertyBlock _propertyBlock;
    MaterialPropertyBlock propertyBlock
    {
        get
        {
            if (_propertyBlock == null)
                _propertyBlock = new MaterialPropertyBlock();
                
            return _propertyBlock;
        }
    }
    
    // Standard Surface Shader: "_MainTex_ST"
    // URP Lit: "_BaseMap_ST"
    public void SetTiling(Vector2 value)
    {
        if (!rend) return;
        rend.GetPropertyBlock(propertyBlock);
        propertyBlock.SetVector(property, new Vector4(value.x, value.y, 0, 0));
        rend.SetPropertyBlock(propertyBlock);        
    }
}
