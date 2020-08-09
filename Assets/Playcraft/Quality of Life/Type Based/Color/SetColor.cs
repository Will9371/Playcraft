using Playcraft;
using UnityEngine;

public class SetColor : MonoBehaviour
{
    [SerializeField] Renderer renderer;
    public void Input(ColorSO value) { Input(value.value); }
    public void Input(Color value) { renderer.material.color = value; }
}
