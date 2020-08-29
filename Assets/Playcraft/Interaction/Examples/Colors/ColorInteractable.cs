using UnityEngine;

namespace Playcraft
{
    public class ColorInteractable : MonoBehaviour
    {
        #pragma warning disable 0649
        [SerializeField] ColorEvent Color;
        [SerializeField] Color defaultColor;
        [SerializeField] Color highlightColor;
        [SerializeField] Color activeColor;
        #pragma warning restore 0649
        
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
                Color.Invoke(activeColor);
            else if (highlight && !active)
                Color.Invoke(highlightColor);
            else
                Color.Invoke(defaultColor);
        }
    }
}
