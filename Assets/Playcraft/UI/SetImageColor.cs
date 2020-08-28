using Playcraft;
using UnityEngine;
using UnityEngine.UI;

public class SetImageColor : MonoBehaviour
{
    [SerializeField] Image image = default;
    public void SetColor(ColorSO value) { SetColor(value.value); }
    public void SetColor(Color value) { image.color = value; }
}
