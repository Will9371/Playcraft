using System;
using UnityEngine;

[Serializable]
public class PropertyBlockInterface
{
    [SerializeField] Renderer rend;

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
    
    public void SetTiling(Vector2 value)
    {
        if (!rend) return;
        rend.GetPropertyBlock(propertyBlock);
        propertyBlock.SetVector("_MainTex_ST", new Vector4(value.x, value.y, 0, 0));
        rend.SetPropertyBlock(propertyBlock);        
    }
}
