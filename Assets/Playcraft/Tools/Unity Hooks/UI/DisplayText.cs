using UnityEngine;
using UnityEngine.UI;

public class DisplayText : MonoBehaviour
{
    [SerializeField] Text display;
    
    public void Display(string value) { display.text = value; }
    public void Display(int value) { display.text = value.ToString(); }
}
