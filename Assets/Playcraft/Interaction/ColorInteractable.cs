using UnityEngine;

public class ColorInteractable : MonoBehaviour
{
    [SerializeField] SetColor color;
    [SerializeField] Color defaultColor;
    [SerializeField] Color highlightColor;
    [SerializeField] Color activeColor;
    
    public bool highlight;
    public bool active;
    
    public void SetHighlight(bool value)
    {
        highlight = value;
        Refresh();
    }
    
    public void SetActive(bool value)
    {
        active = value;
        Refresh();
    }
    
    void Refresh()
    {
        if (active)
            color.Input(activeColor);
        else if (highlight && !active)
            color.Input(highlightColor);
        else
            color.Input(defaultColor);
    }
}
