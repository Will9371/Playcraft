using System;
using Playcraft;
using UnityEngine;
using UnityEngine.Events;

public class RendererInterface : MonoBehaviour
{
    [SerializeField] Renderer rend;
    [SerializeField] string shaderColorId;

    public void SetColor(ColorSO color) { SetColor(color.Value); } 
    public void SetColor(Color color) { rend.material.color = color; }
    
    public void SetMaterialColor(ColorSO color) { SetMaterialColor(color.Value); }
    public void SetMaterialColor(Color color)
    {
        rend.material.SetColor(shaderColorId, color);
    }
}