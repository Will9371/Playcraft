using UnityEngine;
using UnityEngine.UI;

namespace Playcraft
{
    public class DisplayText : MonoBehaviour
    {
        [SerializeField] Text display = default;
        
        public void Display(string value) { display.text = value; }
        public void Display(int value) { display.text = value.ToString(); }
    }
}