using UnityEngine;
using UnityEngine.UI;

namespace ZMD
{
    public class DisplayText : MonoBehaviour
    {
        [SerializeField] Text display = default;
        
        public void Display(string value) { display.text = value; }
        public void Display(int value) { display.text = value.ToString(); }
        public void DisplayF0(float value) { display.text = value.ToString("F0"); }
        public void DisplayF2(float value) { display.text = value.ToString("F2"); }
        public void DisplayFloor(float value) { display.text = Mathf.FloorToInt(value).ToString(); }
        public void DisplayCeil(float value) { display.text = Mathf.CeilToInt(value).ToString(); }
        
        public void DisplayForTime(string value, float time)
        {
            display.text = value;
            CancelInvoke(nameof(Clear));
            Invoke(nameof(Clear), time);
        }
        
        void Clear() { display.text = ""; }
    }
}