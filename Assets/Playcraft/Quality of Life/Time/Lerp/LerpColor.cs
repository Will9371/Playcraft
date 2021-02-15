using UnityEngine;

namespace Playcraft
{
    public class LerpColor : MonoBehaviour
    {
        [Header("Settings")]
        [SerializeField] int defaultIndex;
        [SerializeField] Color[] colors;
        [SerializeField] ColorEvent Output;
        
        [Header("Debug")]
        public Color start;
        public Color end;
        int index;
        
        void Start()
        {            
            if (colors.Length > 0)
            {
                index = defaultIndex;
                start = colors[defaultIndex];
                end = start;
                Input(0f);
            }
        }
            
        // Move between internally-stored positions
        public void SetDestination(int newIndex)
        {
            if (newIndex >= colors.Length)
            {
                Debug.LogError("Attempting to set color index " + newIndex + " of " + colors.Length);
                return;
            }
            
            start = colors[index];
            end = colors[newIndex];
            index = newIndex;
        }
            
        // Move towards externally defined location
        public void SetTargetColor(Color targetColor)
        {
            start = end;
            end = targetColor;
        }
            
        // Call continuously to move over time
        public void Input(float percent) { Output.Invoke(Color.Lerp(start, end, percent)); }
    }
}
