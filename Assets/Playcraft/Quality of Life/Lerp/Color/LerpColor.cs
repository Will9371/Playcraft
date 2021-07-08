using System;
using UnityEngine;

namespace Playcraft
{
    [Serializable] public class LerpColor
    {
        public Color start;
        public Color end;
        public bool reverse;
        
        public ColorEvent Output;

        public void Input(float percent) 
        { 
            if (reverse) percent = 1f - percent;
            Output.Invoke(Color.Lerp(start, end, percent));
        }
        
        public void SetTargetColor(Color targetColor)
        {
            start = end;
            end = targetColor;
        }
    }
}
