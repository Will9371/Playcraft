using UnityEngine;

namespace Playcraft
{
    public class DynamicBar : MonoBehaviour
    {
        enum FillType { Horizontal, Vertical }

        #pragma warning disable 0649
        [SerializeField] RectTransform rect;
        [SerializeField] Vector2 maxSize;
        [SerializeField] FillType fillType;
        [SerializeField] [Range(0, 1)] float fill;
        #pragma warning restore 0649
        
        void OnValidate()
        {
            Refresh();
        }
        
        public void SetFill(float value) 
        { 
            fill = value; 
            Refresh();
        }
        
        void Refresh()
        {
            var x = maxSize.x;
            var y = maxSize.y;
            
            switch (fillType)
            {
                case FillType.Horizontal:
                    x = maxSize.x * fill;
                    break;
                case FillType.Vertical:
                    y = maxSize.y * fill;
                    break;
            }
        
            rect.sizeDelta = new Vector2(x, y);        
        }
    }
}
