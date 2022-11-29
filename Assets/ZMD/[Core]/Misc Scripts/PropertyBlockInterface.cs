using System;
using UnityEngine;

[Serializable]
public class PropertyBlockInterface
{
    [SerializeField] Renderer rend;
    
    [Tooltip("Standard Surface Shader: '_MainTex_ST', " +
             "URP Lit: '_BaseMap_ST'")]
    [SerializeField] string property = "_BaseMap_ST";

    MaterialPropertyBlock _propertyBlock;
    MaterialPropertyBlock propertyBlock
    {
        get
        {
            _propertyBlock ??= new MaterialPropertyBlock();
            return _propertyBlock;
        }
    }
    

    public void SetTiling(Vector2 value)
    {
        if (!rend) return;
        rend.GetPropertyBlock(propertyBlock);
        propertyBlock.SetVector(property, new Vector4(value.x, value.y, 0, 0));
        rend.SetPropertyBlock(propertyBlock);        
    }
}
